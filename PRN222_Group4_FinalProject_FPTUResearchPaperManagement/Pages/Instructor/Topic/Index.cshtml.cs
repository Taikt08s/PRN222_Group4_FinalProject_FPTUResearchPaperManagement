using System.Security.Claims;
using BusinessObject.Enums;
using BusinessObject.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Dtos;
using Service.Interfaces;

namespace PRN222_Group4_FinalProject_FPTUResearchPaperManagement.Pages.Instructor;

[Authorize(Roles = "Instructor")]
public class TopicManagementModel : PageModel
{
    private ITopicService _topicService { get; }
    private ISemesterService _semesterService { get; }

    [BindProperty(SupportsGet = true)]
    public string? SemesterTermFilter { get; set; } = null;

    [BindProperty(SupportsGet = true)]
    public string? GroupStatusFilter { get; set; } = null;

    [BindProperty(SupportsGet = true)]
    public string? TopicStatusFilter { get; set; } = null;

    public List<string> AvailableSemesters { get; set; } = new List<string>();

    public IList<TopicResponseModel> Topics { get; set; } = new List<TopicResponseModel>();
    public int PageIndex { get; set; }
    public int TotalPages { get; set; }
    private const int PageSize = 20;

    public string? ErrorMessage { get; set; }

    public TopicManagementModel(ITopicService topicService, ISemesterService semesterService)
    {
        _semesterService = semesterService;
        _topicService = topicService;
        ErrorMessage = null;
    }

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
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
            return RedirectToPage("/Authentication/Login");
        }
        PageIndex = pageIndex > 0 ? pageIndex : 1;

        Console.WriteLine("Guid after parse: " + id.ToString());
        TopicFilter filter = new()
        {
            TopicStatusFilter = TopicStatusFilter == null ? null : new() { TopicStatusFilter },
            GroupStatusFilter = GroupStatusFilter,
            SemesterTermFilter = SemesterTermFilter == null ? null : new() { SemesterTermFilter },
            ByIntructors = new() { id }
        };

        AvailableSemesters = await _semesterService.GetAllTermAsync();
        var totalTopics = await _topicService.CountAsync(filter);
        TotalPages = (int)Math.Ceiling(totalTopics / (double)PageSize);

        if (PageIndex > TotalPages && TotalPages > 0)
        {
            PageIndex = TotalPages;
        }

        Topics = await _topicService.GetPaginationAsync(filter, PageIndex, PageSize);
        return Page();
    }

    public string GetVietnameseStatus(string statusEnglish)
    {
        // Cố gắng chuyển chuỗi trạng thái thành Enum (để đảm bảo tính an toàn)
        if (Enum.TryParse<TopicStatus>(statusEnglish, true, out var statusEnum))
        {
            return statusEnum switch
            {
                TopicStatus.Created => "Mới Tạo",
                TopicStatus.PendingAssignedGroup => "Chờ Xác Nhận (Nhóm)",
                TopicStatus.PendingAssignedSingle => "Chờ Xác nhận (Cá Nhân)",
                TopicStatus.Assigned => "Đã Giao",
                TopicStatus.Closed => "Đã Đóng",
                _ => statusEnglish // Mặc định trả về giá trị tiếng Anh nếu không khớp
            };
        }
        // Nếu không thể phân tích thành Enum (ví dụ: chuỗi rỗng/lỗi), trả về chính nó.
        return statusEnglish;
    }

}
