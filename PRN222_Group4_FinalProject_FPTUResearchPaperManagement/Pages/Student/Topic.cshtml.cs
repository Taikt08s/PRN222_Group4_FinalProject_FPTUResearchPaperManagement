using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Interfaces;
using System.Security.Claims;
using Service.Dtos;

namespace PRN222_Group4_FinalProject_FPTUResearchPaperManagement.Pages.Student
{
    public class TopicModel : PageModel
    {
        private readonly ITopicService _topicService;

        public TopicModel(ITopicService topicService)
        {
            _topicService = topicService;
        }

        public List<TopicResponseModel> Topics { get; set; } = new();

        public bool AlreadyHasTopic { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return RedirectToPage("/Authentication/Login");
            }

            Guid userIdConvert = Guid.Parse(userId);

            var major = User.FindFirst("major")?.Value;
            if (major == null)
                return RedirectToPage("/Authentication/Login");

            Topics = await _topicService.GetTopicsForStudentAsync(major);

            AlreadyHasTopic = await _topicService.StudentHasTopicAsync(userIdConvert);

            return Page();
        }
    }
}
