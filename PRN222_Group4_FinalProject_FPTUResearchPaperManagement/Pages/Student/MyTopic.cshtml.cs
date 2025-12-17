using BusinessObject.Enums;
using BusinessObject.Models;
using Google.Api.Gax.ResourceNames;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using PRN222_Group4_FinalProject_FPTUResearchPaperManagement.Hubs;
using Service;
using Service.Dtos;
using Service.Interfaces;
using System.Security.Claims;

namespace PRN222_Group4_FinalProject_FPTUResearchPaperManagement.Pages.Student
{
    public class MyTopicModel : PageModel
    {
        private readonly ITopicService _topicService;
        private readonly ISubmissionService _submissionService;
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly IReviewLogService _reviewLogService;

        public MyTopicModel(
            ITopicService topicService,
            ISubmissionService submissionService,
            IHubContext<NotificationHub> hubContext,
            IReviewLogService reviewLogService)
        {
            _hubContext = hubContext;
            _topicService = topicService;
            _submissionService = submissionService;
            _reviewLogService = reviewLogService;
        }

        public TopicResponseModel RegisteredTopic { get; set; }

        public SubmissionDto Submission { get; set; }

        [BindProperty]
        public IFormFile UploadFile { get; set; }

        [BindProperty]
        public string FolderName { get; set; }

        public List<SubmissionFileDto> Files { get; set; } = new();
        public string CurrentFolder { get; set; }
        public List<BusinessObject.Models.ReviewLog> ReviewLogs { get; set; } = new();
        public Dictionary<string, BusinessObject.Models.ReviewLog?> LatestVotes { get; set; } = new();
        public ReviewStatus InstructorStatus { get; set; } = ReviewStatus.Pending;
        public string? InstructorComment { get; set; } = null;
        public ReviewStatus GpecStatus { get; set; } = ReviewStatus.Pending;
        public string? GpecComment { get; set; } = null;

        public ReviewStatus SubmissionStatus =>
            (InstructorStatus == ReviewStatus.Approved &&
             GpecStatus == ReviewStatus.Approved)
                ? ReviewStatus.Approved
                : (InstructorStatus == ReviewStatus.Rejected ||
                   GpecStatus == ReviewStatus.Rejected)
                    ? ReviewStatus.Rejected
                    : ReviewStatus.Pending;

        public async Task<IActionResult> OnGet(string? folder)
        {
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdStr))
                return RedirectToPage("/Authentication/Login");

            var userId = Guid.Parse(userIdStr);

            RegisteredTopic = await _topicService.GetRegisteredTopicForStudentAsync(userId);
            if (RegisteredTopic == null)
                return Page();

            Submission = await _submissionService.GetOrCreateSubmissionAsync(
                RegisteredTopic.Id,
                RegisteredTopic.GroupId,
                RegisteredTopic.SemesterId);

            Files = await _submissionService.GetFilesAsync(Submission.Id);

            if (Submission?.Id > 0)
            {
                ReviewLogs = await _reviewLogService.GetBySubmissionIdAsync(Submission.Id);

                LatestVotes = ReviewLogs
                    .Where(r => r.Reviewer != null)
                    .GroupBy(r => r.Reviewer.Role)
                    .ToDictionary(
                        g => g.Key,
                        g => g.OrderByDescending(l => l.Created_At).FirstOrDefault()
                    );

                InstructorStatus = GetReviewStatus(LatestVotes, "Instructor");

                InstructorComment = GetReviewComment(LatestVotes, "Instructor");

                GpecStatus = GetReviewStatus(LatestVotes, "GraduationProjectEvaluationCommitteeMember");
                GpecComment = GetReviewComment(LatestVotes, "GraduationProjectEvaluationCommitteeMember");

            }

            CurrentFolder = folder;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (UploadFile == null || UploadFile.Length == 0)
                return Page();

            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdStr))
                return RedirectToPage("/Authentication/Login");

            var userId = Guid.Parse(userIdStr);

            RegisteredTopic = await _topicService.GetRegisteredTopicForStudentAsync(userId);

            if (RegisteredTopic == null)
                return Page();

            var submission = await _submissionService.GetOrCreateSubmissionAsync(
                RegisteredTopic.Id,
                RegisteredTopic.GroupId,
                RegisteredTopic.SemesterId
            );

            using var stream = UploadFile.OpenReadStream();

            await _submissionService.UploadFileAsync(
                submission.Id,
                FolderName,
                stream,
                UploadFile.FileName,
                UploadFile.ContentType
            );

            return RedirectToPage(new { folder = FolderName });
        }

        public async Task<IActionResult> OnPostSubmitFinalAsync()
        {
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdStr))
                return RedirectToPage("/Authentication/Login");

            var userId = Guid.Parse(userIdStr);

            RegisteredTopic = await _topicService.GetRegisteredTopicForStudentAsync(userId);
            if (RegisteredTopic == null)
                return Page();

            var submission = await _submissionService.GetOrCreateSubmissionAsync(
                RegisteredTopic.Id,
                RegisteredTopic.GroupId,
                RegisteredTopic.SemesterId
            );

            if (submission.Status == "Submitted" || submission.Status == "Suspended")
                return RedirectToPage();

            await _submissionService.SubmitAsync(submission.Id);
            await _hubContext.Clients.Group("Instructors")
                .SendAsync("SubmissionSubmitted", new
                {
                    topicId = RegisteredTopic.Id,
                    groupId = RegisteredTopic.GroupId,
                    submissionId = submission.Id,
                    submissionStatus = "Submitted"
                });

            TempData["Success"] = "Nộp bài thành công!";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteFileAsync(int id, string folderName)
        {
            await _submissionService.RemoveFileAsync(id);
            return RedirectToPage(new { folder = folderName });
        }

        private static ReviewStatus GetReviewStatus(
                Dictionary<string, ReviewLog?> latestVotes,
                string role)
        {
            if (!latestVotes.TryGetValue(role, out var vote) || vote == null)
                return ReviewStatus.Pending;

            return vote.New_Status?.ToLower() switch
            {
                "approve" => ReviewStatus.Approved,
                "reject" => ReviewStatus.Rejected,
                "suspend" => ReviewStatus.Suspended,
                _ => ReviewStatus.Pending
            };
        }

        private static string? GetReviewComment(
                Dictionary<string, ReviewLog?> latestVotes,
                string role)
        {
            if (!latestVotes.TryGetValue(role, out var vote) || vote == null)
                return null;

            return vote.Comment;
        }

    }
}
