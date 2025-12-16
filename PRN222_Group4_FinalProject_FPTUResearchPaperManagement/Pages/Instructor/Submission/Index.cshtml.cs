using System.Security.Claims;
using BusinessObject.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Dtos;
using Service.Interfaces;

namespace PRN222_Group4_FinalProject_FPTUResearchPaperManagement.Pages.Instructor.Submission
{
    [Authorize(Roles = "Instructor,GraduationProjectEvaluationCommitteeMember")]
    public class IndexModel : PageModel
    {
        private readonly ITopicService _topicService;

        public IndexModel(ITopicService topicService)
        {
            _topicService = topicService;
        }

        public List<TopicResponseModel> Topics { get; set; } = new();

        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 50;

        private Guid GetUserGuid()
        {
            string? id = HttpContext.User
        .FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(id))
            {
                throw new Exception("Cannot get user id");
            }
            Console.WriteLine("String id: " + id);
            return Guid.Parse(id);
        }

        public async Task<IActionResult> OnGetAsync(int pageIndex = 1)
        {
            Guid id;
            try
            {
                id = GetUserGuid();
            }
            catch
            {
                return RedirectToPage("/Authentication/Login");
            }
            PageIndex = pageIndex;
            TopicFilter filter = new()
            {
                ByIntructors = new() { GetUserGuid() }
            };
            Topics = await _topicService.GetPaginationAsync(filter, PageIndex, PageSize);

            ViewData["ShowSidebar"] = true;
            ViewData["ActiveMenu"] = "TopicSubmissions";
            return Page();
        }
    }
}
