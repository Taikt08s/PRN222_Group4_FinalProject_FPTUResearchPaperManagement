using BusinessObject.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Interfaces;

namespace PRN222_Group4_FinalProject_FPTUResearchPaperManagement.Pages.Student;

public class TopicDetail : PageModel
{
    private readonly ITopicService _topicService;

    public TopicDetail(ITopicService topicService)
    {
        _topicService = topicService;
    }

    public TopicResponseModel? Topic { get; set; }

    public async Task<IActionResult> OnGet(int id)
    {
        Topic = await _topicService.GetTopicByIdAsync(id);

        if (Topic == null)
            return RedirectToPage("/Student/Topic");

        return Page();
    }
}