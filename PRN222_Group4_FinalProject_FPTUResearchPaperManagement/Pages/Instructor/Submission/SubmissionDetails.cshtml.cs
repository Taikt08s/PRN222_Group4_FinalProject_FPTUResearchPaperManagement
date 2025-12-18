using BusinessObject.Enums;
using BusinessObject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using PRN222_Group4_FinalProject_FPTUResearchPaperManagement.Hubs;
using Service.Dtos;
using Service.Interfaces;
using System.Security.Claims;

namespace PRN222_Group4_FinalProject_FPTUResearchPaperManagement.Pages.Instructor.Submission
{
    [Authorize(Roles = "Instructor")]
    public class SubmissionDetailsModel : PageModel
    {
        private ISubmissionService _submissionService { get; }
        private IReviewLogService _reviewLogService { get; }
        private IHubContext<NotificationHub> _hubContext { get; }
        private IStudentGroupService _studentGroupService { get; }

        public SubmissionDetailsModel(
            IStudentGroupService studentGroupService,
            ISubmissionService submissionService,
            IReviewLogService reviewLogService,
            IHubContext<NotificationHub> hubContext)
        {
            _studentGroupService = studentGroupService;
            _submissionService = submissionService;
            _reviewLogService = reviewLogService;
            _hubContext = hubContext;
        }

        public SubmissionDto? Submission { get; set; }
        public ThesisAiResult? ThesisAiResult { get; set; }
        public List<SubmissionFileDto> Files { get; set; } = new();
        public List<(Guid Id, string FullName, string Email, bool IsLeader)> Members { get; set; } = new();

        // review data
        public List<ReviewLog> ReviewLogs { get; set; } = new();
        public Dictionary<string, ReviewLog?> LatestVotes { get; set; } = new();

        // current user vote
        public ReviewLog? CurrentUserLatestVote { get; set; }
        public bool HasCurrentUserApproved { get; set; }

        [BindProperty(SupportsGet = true)]
        public int SubmissionId { get; set; }
        
        public bool IsFullyApproved { get; set; }

        public async Task<IActionResult> OnGetAsync(int submissionId)
        {
            if (submissionId <= 0) return BadRequest();

            SubmissionId = submissionId;

            Submission = await _submissionService.GetByIdAsync(submissionId);
            if (Submission == null) return NotFound();
            
            ThesisAiResult = await _submissionService.GetThesisAiResultAsync(submissionId);

            Files = await _submissionService.GetFilesAsync(submissionId);

            // load members for group
            if (Submission.GroupId > 0)
            {
                var group = await _studentGroupService.GetByIdAsync(Submission.GroupId);

                if (group?.Members != null)
                {
                    Members = group.Members
                        .Select(m => (m.Student_Id, m.Student?.Full_Name ?? "", m.Student?.Email ?? "", m.Is_Leader))
                        .ToList();
                }
            }

            // review logs via service
            ReviewLogs = await _reviewLogService.GetBySubmissionIdAsync(submissionId);

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
                                         && string.Equals(CurrentUserLatestVote.New_Status, nameof(ReviewStatus.Approved), StringComparison.OrdinalIgnoreCase);
            }
            
            var instructorApproved =
                LatestVotes.TryGetValue("Instructor", out var instructorVote)
                && instructorVote?.New_Status == "Approve";

            var gpecApproved =
                LatestVotes.TryGetValue("GraduationProjectEvaluationCommitteeMember", out var gpecVote)
                && gpecVote?.New_Status == "Approve";

            IsFullyApproved = instructorApproved && gpecApproved;


            ViewData["ShowSidebar"] = true;
            ViewData["ActiveMenu"] = "TopicSubmissions";

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

                // broadcast final/partial update so UI clients (students/instructors/GPEC) can react
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
                // redirect back to details so UI refreshes and shows current user's disabled approve button
                return RedirectToPage(new { submissionId = SubmissionId });
            }
            catch (InvalidOperationException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                ModelState.AddModelError(string.Empty, ex.Message);
                await OnGetAsync(SubmissionId);
                return Page();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                ModelState.AddModelError(string.Empty, "Review failed: " + ex.Message);
                await OnGetAsync(SubmissionId);
                return Page();
            }
        }
    }
}
