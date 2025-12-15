using Microsoft.AspNetCore.Authorization;
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

        public async Task OnGetAsync(int pageIndex = 1)
        {
            PageIndex = pageIndex;
            Topics = await _topicService.GetPaginationAsync(null, PageIndex, PageSize);

            ViewData["ShowSidebar"] = true;
            ViewData["ActiveMenu"] = "TopicSubmissions";
        }
    }
}