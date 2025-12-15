using Google.Api.Gax.ResourceNames;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Dtos;
using Service.Interfaces;
using System.Security.Claims;

namespace PRN222_Group4_FinalProject_FPTUResearchPaperManagement.Pages.Student
{
    public class MyTopicModel : PageModel
    {
        private readonly ITopicService _topicService;
        private readonly ISubmissionService _submissionService;

        public MyTopicModel(ITopicService topicService, ISubmissionService submissionService)
        {
            _topicService = topicService;
            _submissionService = submissionService;
        }

        public TopicResponseModel RegisteredTopic { get; set; }

        public SubmissionDto Submission { get; set; }

        [BindProperty]
        public IFormFile UploadFile { get; set; }

        [BindProperty]
        public string FolderName { get; set; }

        public List<SubmissionFileDto> Files { get; set; } = new();
        public string CurrentFolder { get; set; }
        

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

            TempData["Success"] = "Nộp bài thành công!";
            return RedirectToPage();
        }
    }
}