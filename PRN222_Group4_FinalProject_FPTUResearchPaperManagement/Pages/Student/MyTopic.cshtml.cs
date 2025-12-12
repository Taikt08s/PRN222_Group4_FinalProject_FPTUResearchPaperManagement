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

        public MyTopicModel(ITopicService topicService)
        {
            _topicService = topicService;
        }

        public TopicResponseModel RegisteredTopic { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdStr))
                return RedirectToPage("/Authentication/Login");

            var userId = Guid.Parse(userIdStr);

            RegisteredTopic = await _topicService.GetRegisteredTopicForStudentAsync(userId);

            return Page();
        }
    }
}
