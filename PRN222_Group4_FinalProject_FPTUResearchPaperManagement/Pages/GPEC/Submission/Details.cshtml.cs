using BusinessObject.Models;
using DataAccessLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using PRN222_Group4_FinalProject_FPTUResearchPaperManagement.Hubs;
using Service;
using Service.Dtos;
using Service.Interfaces;
using System.Security.Claims;

namespace PRN222_Group4_FinalProject_FPTUResearchPaperManagement.Pages.GPEC.Submission
{
    [Authorize(Roles = "GraduationProjectEvaluationCommitteeMember")]
    public class DetailsModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly ISubmissionService _submissionService;
        private readonly IReviewLogService _reviewLogService;
        private readonly IHubContext<NotificationHub> _hubContext;

        public DetailsModel(AppDbContext context, ISubmissionService submissionService, IReviewLogService reviewLogService, IHubContext<NotificationHub> hubContext)
        {
            _context = context;
            _submissionService = submissionService;
            _reviewLogService = reviewLogService;
            _hubContext = hubContext;
        }

        public Service.Dtos.SubmissionDto Submission { get; set; }
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

            var sub = await _context.Submissions
                .Include(s => s.Group)
                    .ThenInclude(g => g.Members)
                        .ThenInclude(m => m.Student)
                .Include(s => s.Files)
                .Include(s => s.Topic)
                .FirstOrDefaultAsync(s => s.Id == submissionId);

            if (sub == null) return NotFound();

            // map simple DTOs
            Submission = new Service.Dtos.SubmissionDto
            {
                Id = sub.Id,
                GroupId = sub.Group_Id,
                TopicId = sub.Topic_Id,
                SemesterId = sub.Semester_Id,
                Status = sub.Status,
                SubmittedAt = sub.Submitted_At,
                ReviewedAt = sub.Reviewed_At,
                PlagiarismFlag = sub.Plagiarism_Flag,
                PlagiarismScore = sub.Plagiarism_Score,
                RejectReason = sub.Reject_Reason
            };

            Files = sub.Files.Select(f => new Service.Dtos.SubmissionFileDto
            {
                Id = f.Id,
                File_Name = f.File_Name,
                File_Type = f.File_Type,
                Firebase_Url = f.Firebase_Url,
                Uploaded_At = f.Uploaded_At
            }).ToList();

            if (sub.Group?.Members != null)
            {
                Members = sub.Group.Members.Select(m => (m.Student_Id, m.Student?.Full_Name ?? "", m.Student?.Email ?? "", m.Is_Leader)).ToList();
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
