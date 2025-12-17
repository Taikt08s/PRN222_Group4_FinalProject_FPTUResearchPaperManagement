using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer;

namespace PRN222_Group4_FinalProject_FPTUResearchPaperManagement.Pages.GPEC.Submission
{
    [Authorize(Roles = "GraduationProjectEvaluationCommitteeMember")]
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public class SubmissionListItem
        {
            public int SubmissionId { get; set; }
            public string TopicTitle { get; set; }
            public int GroupId { get; set; }
            public int MemberCount { get; set; }
            public string Status { get; set; }
            public bool PlagiarismFlag { get; set; }
            public bool ReviewedByMe { get; set; }
        }

        public List<SubmissionListItem> Submissions { get; set; } = new();

        public async Task OnGetAsync()
        {
            ViewData["ShowSidebar"] = true;
            ViewData["ActiveMenu"] = "TopicSubmissions";

            var userIdStr = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            Guid? currentUserId = Guid.TryParse(userIdStr, out var g) ? g : null;

            var list = await _context.Submissions
                .Include(s => s.Topic)
                .Include(s => s.Group)
                    .ThenInclude(g => g.Members)
                .Include(s => s.ReviewLogs) // IMPORTANT
                .Where(s => s.Status == "Submitted"
                         || s.Status == "Reviewing"
                         || s.Status == "Suspended"
                         || s.Status == "Approved")
                .ToListAsync();

            Submissions = list.Select(s => new SubmissionListItem
            {
                SubmissionId = s.Id,
                TopicTitle = s.Topic?.Title ?? "",
                GroupId = s.Group_Id,
                MemberCount = s.Group?.Members?.Count ?? 0,
                Status = s.Status,
                PlagiarismFlag = s.Plagiarism_Flag,

                ReviewedByMe = currentUserId != null &&
                    s.ReviewLogs.Any(r =>
                        r.Reviewer_Id == currentUserId &&
                        r.Reviewer.Role == "GraduationProjectEvaluationCommitteeMember")
            }).ToList();
        }

    }
}
