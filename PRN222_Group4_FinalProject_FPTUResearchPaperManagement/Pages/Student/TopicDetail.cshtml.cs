using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Dtos;
using Service.Interfaces;

namespace PRN222_Group4_FinalProject_FPTUResearchPaperManagement.Pages.Student;

public class TopicDetail : PageModel
{
    private readonly ITopicService _topicService;
    private readonly IUserService _userService;

    public TopicDetail(ITopicService topicService, IUserService userService)
    {
        _topicService = topicService;
        _userService = userService;
    }

    public TopicResponseModel? Topic { get; set; }

    [BindProperty]
    public TopicRegistrationRequest Registration { get; set; } = new();


    public async Task<IActionResult> OnGet(int id)
    {
        Topic = await _topicService.GetTopicByIdAsync(id);

        if (Topic == null)
            return RedirectToPage("/Student/Topic");

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var userId = Guid.Parse(User.FindFirst("user_id").Value);

        Registration.StudentId = userId;

        var result = await _topicService.TopicRegistrationAsync(Registration);

        if (!result.Success)
        {
            ModelState.AddModelError(string.Empty, result.Error);

            Topic = await _topicService.GetTopicByIdAsync(Registration.TopicId);

            return Page();
        }

        return RedirectToPage("/Student/Topic");
    }


    public async Task<JsonResult> OnGetFindStudentAsync(string email)
    {
        var student = await _userService.FindStudentByEmailAsync(email);

        if (student == null)
            return new JsonResult(null);

        return new JsonResult(student);
    }
}