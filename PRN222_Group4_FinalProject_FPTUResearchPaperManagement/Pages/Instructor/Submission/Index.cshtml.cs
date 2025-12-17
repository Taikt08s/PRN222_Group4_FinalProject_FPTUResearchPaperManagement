using System.Security.Claims;
using BusinessObject.Filters;
using DataAccessLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Service.Dtos;
using Service.Interfaces;

namespace PRN222_Group4_FinalProject_FPTUResearchPaperManagement.Pages.Instructor.Submission
{
    [Authorize(Roles = "Instructor")]
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly ITopicService _topicService;
        private readonly ISubmissionService _submissionService;

        public IndexModel(
            AppDbContext context,
            ITopicService topicService,
            ISubmissionService submissionService)
        {
            _context = context;
            _topicService = topicService;
            _submissionService = submissionService;
        }

        public List<TopicSubmissionRow> Rows { get; set; } = new();
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalPages { get; set; }

        public async Task<IActionResult> OnGetAsync(int pageIndex = 1)
        {
            ViewData["ShowSidebar"] = true;
            ViewData["ActiveMenu"] = "TopicSubmissions";

            var instructorId = Guid.Parse(
                User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value
            );

            TopicFilter filter = new()
            {
                ByIntructors = new() { instructorId }
            };
            var topics = await _topicService.GetPaginationAsync(filter, pageIndex, PageSize);

            foreach (var t in topics)
            {
                var group = await _context.StudentGroups
                    .Where(g => g.Topic_Id == t.Id)
                    .Include(g => g.Members)
                        .ThenInclude(m => m.Student)
                    .FirstOrDefaultAsync();

                int? groupId = group?.Id;
                string? leader = group?.Members
                    .FirstOrDefault(m => m.Is_Leader)?.Student?.Full_Name;

                var submission = group == null
                    ? null
                    : await _submissionService.GetSubmissionAsync(
                        t.Id,
                        group.Id,
                        t.SemesterId
                    );


                var userIdStr = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                Guid? currentUserId = Guid.TryParse(userIdStr, out var g) ? g : null;

                var reviewedByMe =
                        await _context.ReviewLogs.AnyAsync(r =>
                            r.Reviewer_Id == currentUserId &&
                            submission != null ? r.Created_At >= submission.SubmittedAt : true &&
                            r.Reviewer.Role == "Instructor");
                Rows.Add(new TopicSubmissionRow
                {
                    TopicId = t.Id,
                    TopicTitle = t.Title,
                    Major = t.Major,
                    Semester = t.SemesterTerm,

                    GroupId = groupId,
                    LeaderName = leader,
                    MemberCount = group?.Members.Count ?? 0,

                    SubmissionId = submission?.Id,
                    SubmissionStatus = submission?.Status ?? "Not submitted",
                    HasReviewedByMe = currentUserId != null && reviewedByMe,
                });
            }

            return Page();
        }
    }

}
