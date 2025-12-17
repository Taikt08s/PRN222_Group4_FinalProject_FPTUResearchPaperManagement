using BusinessObject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using PRN222_Group4_FinalProject_FPTUResearchPaperManagement.Hubs;
using Service.Interfaces;
using System.Security.Claims;

namespace PRN222_Group4_FinalProject_FPTUResearchPaperManagement.Pages.GPEC.Submission
{
    [Authorize(Roles = "GraduationProjectEvaluationCommitteeMember")]
    public class DetailsModel : PageModel
    {
        private readonly ISubmissionService _submissionService;
        private readonly IReviewLogService _reviewLogService;
        private readonly IStudentGroupMemberService _studentGroupMemberService;
        private readonly IHubContext<NotificationHub> _hubContext;

        public DetailsModel(ISubmissionService submissionService, IReviewLogService reviewLogService, IHubContext<NotificationHub> hubContext, IStudentGroupMemberService studentGroupMemberService)
        {
            _studentGroupMemberService = studentGroupMemberService;
            _submissionService = submissionService;
            _reviewLogService = reviewLogService;
            _hubContext = hubContext;
        }

        public Service.Dtos.SubmissionDto? Submission { get; set; }
        public List<Service.Dtos.SubmissionFileDto> Files { get; set; } = new();
        public List<(Guid Id, string FullName, string Email, bool IsLeader)> Members { get; set; } = new();

        // Review related
        public List<ReviewLog> ReviewLogs { get; set; } = new();
        public Dictionary<string, ReviewLog?> LatestVotes { get; set; } = new();

        // current user vote info
        public ReviewLog? CurrentUserLatestVote { get; set; }
        public bool HasCurrentUserApproved { get; set; }

        [BindProperty(SupportsGet = true)]
        public int SubmissionId { get; set; }

        public async Task<IActionResult> OnGetAsync(int submissionId)
        {
            SubmissionId = submissionId;
            ViewData["ShowSidebar"] = true;
            ViewData["ActiveMenu"] = "TopicSubmissions";

            var sub = await _submissionService.GetByIdAsync(submissionId);

            if (sub == null) return NotFound();

            // map simple DTOs
            Submission = sub;

            Files = await _submissionService.GetFilesAsync(submissionId);

            var group = await _studentGroupMemberService.GetByTopicAsync(sub.TopicId);
            if (group.Count > 0)
            {
                Members = group.Select(m => (m.Student_Id, m.Student?.Full_Name ?? "", m.Student?.Email ?? "", m.Is_Leader)).ToList();
            }

            // ---- REVIEW LOGS ----
            ReviewLogs = await _reviewLogService.GetBySubmissionIdAsync(submissionId);

            // compute latest vote per reviewer role (Instructor, GraduationProjectEvaluationCommitteeMember, ...)
            LatestVotes = ReviewLogs
                .Where(r => r.Reviewer != null)
                .GroupBy(r => r.Reviewer.Role)
                .ToDictionary(
                    g => g.Key,
                    g => g.OrderByDescending(r => r.Created_At).FirstOrDefault()
                );

            // current user latest vote
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrEmpty(userIdStr) && Guid.TryParse(userIdStr, out var currentUserGuid))
            {
                CurrentUserLatestVote = ReviewLogs
                    .Where(l => l.Reviewer_Id == currentUserGuid)
                    .OrderByDescending(l => l.Created_At)
                    .FirstOrDefault();

                HasCurrentUserApproved = CurrentUserLatestVote != null
                                         && string.Equals(CurrentUserLatestVote.New_Status, "Approve", StringComparison.OrdinalIgnoreCase);
            }
            else
            {
                CurrentUserLatestVote = null;
                HasCurrentUserApproved = false;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostReviewAsync(string decision, string comment)
        {
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdStr)) return Unauthorized();

            var reviewerId = Guid.Parse(userIdStr);

            try
            {
                await _submissionService.ReviewSubmissionAsync(SubmissionId, reviewerId, decision, comment ?? "");

                // fetch updated submission to include latest status/topic/group
                var updated = await _submissionService.GetByIdAsync(SubmissionId);
                if (updated != null)
                {
                    await _hubContext.Clients.All.SendAsync("ReceiveApprovalUpdate", new
                    {
                        topicId = updated.TopicId,
                        groupId = updated.GroupId,
                        submissionId = updated.Id,
                        newStatus = updated.Status,
                        submissionStatus = updated.Status
                    });
                }

                TempData["Message"] = "Your review has been recorded.";
                return RedirectToPage("./Index");
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await OnGetAsync(SubmissionId);
                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Review failed: " + ex.Message);
                await OnGetAsync(SubmissionId);
                return Page();
            }
        }
    }
}
