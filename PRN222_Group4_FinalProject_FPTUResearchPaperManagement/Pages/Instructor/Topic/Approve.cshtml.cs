using BusinessObject.Enums;
using BusinessObject.Filters;
using BusinessObject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using PRN222_Group4_FinalProject_FPTUResearchPaperManagement.Hubs;
using Service.Dtos;
using Service.Interfaces;

namespace PRN222_Group4_FinalProject_FPTUResearchPaperManagement.Pages.Instructor;

[Authorize(Roles = "Instructor")]
public class TopicApproveModel : PageModel
{
    private readonly IHubContext<NotificationHub> _hubContext;
    private ITopicService _topicService { get; }
    private ISemesterService _semesterService { get; }

    public string? ErrorMessage { get; set; }

    public TopicApproveModel(IHubContext<NotificationHub> hubContext, ITopicService topicService, ISemesterService semesterService)
    {
        _hubContext = hubContext;
        _topicService = topicService;
        _semesterService = semesterService;
        ErrorMessage = null;
    }

    public IList<TopicResponseModel> PendingTopics { get; set; } = new List<TopicResponseModel>();

    public int PageIndex { get; set; }
    public int TotalPages { get; set; }
    private const int PageSize = 20;

    public async Task OnGetAsync(int pageIndex = 1)
    {
        PageIndex = pageIndex > 0 ? pageIndex : 1;
        var pendingStatuses = new List<string>
        {
            nameof(TopicStatus.PendingAssignedGroup),
            nameof(TopicStatus.PendingAssignedSingle)
        };
        var currentSemester = await _semesterService.GetCurrentSemester();
        TopicFilter filter = new()
        {
            SemesterTermFilter = currentSemester == null ? null : new() { currentSemester.Term },
            TopicStatusFilter = pendingStatuses

        };
        var totalTopics = await _topicService.CountAsync(filter);
        TotalPages = (int)Math.Ceiling(totalTopics / (double)PageSize);
        if (PageIndex > TotalPages && TotalPages > 0)
        {
            PageIndex = TotalPages;
        }
        PendingTopics = await _topicService.GetPaginationAsync(filter, PageIndex, PageSize);
    }

    // --- Helper Method để dịch trạng thái (tái sử dụng) ---
    public string GetVietnameseStatus(string statusEnglish)
    {
        if (Enum.TryParse<TopicStatus>(statusEnglish, true, out var statusEnum))
        {
            return statusEnum switch
            {
                TopicStatus.Created => "Đã Tạo",
                TopicStatus.PendingAssignedGroup => "Chờ Duyệt (Nhóm)",
                TopicStatus.PendingAssignedSingle => "Chờ Duyệt (Cá Nhân)",
                TopicStatus.Assigned => "Đã Duyệt/Sẵn Sàng Giao",
                TopicStatus.Closed => "Đã Đóng/Từ Chối",
                _ => statusEnglish
            };
        }
        return statusEnglish;
    }


    [ValidateAntiForgeryToken]
    public async Task<IActionResult> OnPostApproveAsync(int id)
    {
        return await ProcessTopicStatusUpdate(id, nameof(TopicStatus.Assigned));
    }

    private async Task<IActionResult> ProcessTopicStatusUpdate(int topicId, string newStatus)
    {
        try
        {
            await _topicService.UpdateTopicStatus(topicId, newStatus);
            string reloadMessage = "Danh sách đề tài chờ duyệt đã được cập nhật.";
            await _hubContext.Clients.All.SendAsync("ReceiveApprovalUpdate", reloadMessage);
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
        return RedirectToPage();
    }
}
