using BusinessObject.Enums;
using BusinessObject.Models;
using DocumentFormat.OpenXml.Packaging;
using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Options;
using OpenAI.Chat;
using Service.Dtos;
using Service.Helpers;
using Service.Interfaces;
using Service.Options;
using System.Text;
using System.Text.Json;

namespace Service;

public class OpenAiSubmissionService : IOpenAiSubmissionService
{
    private readonly ChatClient _chat;
    private readonly StorageClient _storage;

    public OpenAiSubmissionService(IOptions<OpenAiOptions> options,StorageClient storage)
    {
        var cfg = options.Value;
        _chat = new ChatClient(cfg.Model, cfg.ApiKey);
        _storage = storage;
    }
    private static string ExtractDocxText(Stream stream)
    {
        using var wordDoc = WordprocessingDocument.Open(stream, false);
        var body = wordDoc.MainDocumentPart.Document.Body;
        return body.InnerText;
    }

    private static string ExtractObjectName(string firebaseUrl)
    {
        var uri = new Uri(firebaseUrl);
        var path = uri.AbsolutePath.TrimStart('/');
        var bucketPrefix = "fpturesearchpapermanagement.firebasestorage.app/";
        if (!path.StartsWith(bucketPrefix))
            throw new Exception("Invalid Firebase Storage URL");
        var objectNameEncoded = path.Substring(bucketPrefix.Length);
        return Uri.UnescapeDataString(objectNameEncoded);
    }
    
    public async Task<bool> ValidateAndModerateSubmissionAsync(Submission submission)
    {
        var filesToCheck = submission.Files
            .Where(f => SubmissionFileValidator.IsAllowedFolder(f.File_Type))
            .ToList();

        // ===== 1. Filename validation =====
        foreach (var file in filesToCheck)
        {
            bool valid = SubmissionFileValidator.IsValidFileName(
                file.File_Name,
                submission.Group.Id.ToString()
            );

            if (!valid)
            {
                Reject(submission, $"Invalid file name: {file.File_Name}");
                return false;
            }
        }

        // ===== 2. AI content check =====
        var contentBuilder = new StringBuilder();

        foreach (var file in filesToCheck)
        {
            var objectName = ExtractObjectName(file.Firebase_Url);

            using var ms = new MemoryStream();

            await _storage.DownloadObjectAsync(
                "fpturesearchpapermanagement.firebasestorage.app",
                objectName,
                ms
            );

            string text;

            if (file.File_Name.EndsWith(".docx"))
            {
                text = ExtractDocxText(ms);
            }
            else
            {
                text = Encoding.UTF8.GetString(ms.ToArray());
            }

            contentBuilder.AppendLine($"--- FILE: {file.File_Name} ---");
            contentBuilder.AppendLine(text);
        }

        var aiResult = await ModerateWithAiAsync(contentBuilder.ToString());

        UpdateModeration(submission, aiResult);

        return aiResult.IsApproved;
    }
    
    private static string CleanJson(string input)
    {
        input = input.Trim();
        int start = input.IndexOf('{');
        int end = input.LastIndexOf('}');
        if (start < 0 || end < 0) throw new Exception("Invalid AI JSON");
        return input.Substring(start, end - start + 1);
    }
    private async Task<ThesisAiResult> ModerateWithAiAsync(string content)
    {
        var prompt = $@"
            You are an academic integrity system.

            Analyze the thesis content below and return STRICT JSON only.

            Evaluation rules (IMPORTANT):
            - plagiarism_score must be a number from 0 to 100
            - plagiarism_pass MUST be:
              true  if plagiarism_score > 80
              false if plagiarism_score <= 80
            - is_approved MUST be determined as follows:
              • true  → if plagiarism_score <= 80 AND issues are minor or acceptable
              • false → if plagiarism_score > 80 OR violations are serious
            - Moderate scores (60–80) should be treated leniently if originality
              and citation quality are acceptable

            Check:
            1. Plagiarism
            2. Academic originality
            3. Citation quality

            Return format:
            {{
              ""is_approved"": true/false,
              ""plagiarism_score"": 0-100,
              ""plagiarism_pass"": true/false,
              ""reasoning"": ""Vietnamese explanation"",
              ""violations"": [""..."", ""...""]
            }}

            CONTENT:
            {content}
            ";
        var response = await _chat.CompleteChatAsync(prompt);
        var raw = response.Value.Content[0].Text;
        var json = CleanJson(raw);
        var doc = JsonDocument.Parse(json);
        var root = doc.RootElement;

        return new ThesisAiResult
        {
            IsApproved = root.GetProperty("is_approved").GetBoolean(),
            PlagiarismScore = root.GetProperty("plagiarism_score").GetSingle(),
            PlagiarismPass = root.GetProperty("plagiarism_pass").GetBoolean(),
            Reasoning = root.GetProperty("reasoning").GetString() ?? "",
            ViolationsJson = root.GetProperty("violations").GetRawText()
        };
    }

    private void Reject(Submission submission, string reason)
    {
        submission.Status = SubmissionStatus.Rejected.ToString();
        submission.Reject_Reason = reason;
        submission.Plagiarism_Flag = false;
    }

    private void UpdateModeration(Submission submission, ThesisAiResult result)
    {
        submission.Plagiarism_Score = result.PlagiarismScore;
        submission.Plagiarism_Flag = !result.PlagiarismPass;

        submission.Moderation = new ThesisModeration
        {
            Submission_Id = submission.Id,
            Plagiarism_Pass = result.PlagiarismPass,
            Plagiarism_Score = result.PlagiarismScore,
            Is_Approved = result.IsApproved,
            Reasoning = result.Reasoning,
            Violations_Json = result.ViolationsJson,
            Created_At = DateTime.UtcNow
        };

        if (!result.IsApproved)
        {
            submission.Status = SubmissionStatus.Rejected.ToString();
            submission.Reject_Reason = "AI detected plagiarism or violations";
            if (submission.Topic != null)
            {
                submission.Topic.Status = TopicStatus.Closed.ToString();
            }
        }
    }
}