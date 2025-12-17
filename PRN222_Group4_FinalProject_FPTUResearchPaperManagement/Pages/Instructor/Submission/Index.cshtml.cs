using BusinessObject.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Dtos;
using Service.Interfaces;

namespace PRN222_Group4_FinalProject_FPTUResearchPaperManagement.Pages.Instructor.Submission
{
    [Authorize(Roles = "Instructor")]
    public class IndexModel : PageModel
    {
        private readonly ITopicService _topicService;
        private readonly ISubmissionService _submissionService;
        private readonly IStudentGroupService _studentGroupService;
        private readonly IReviewLogService _reviewLogService;

        public IndexModel(
            IReviewLogService reviewLogService,
            ITopicService topicService,
            IStudentGroupService studentGroupService,
            ISubmissionService submissionService)
        {
            _reviewLogService = reviewLogService;
            _studentGroupService = studentGroupService;
            _topicService = topicService;
            _submissionService = submissionService;
        }

        public List<TopicSubmissionRow> Rows { get; set; } = new();
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalPages { get; set; }

        public async Task<IActionResult> OnGetAsync(int pageIndex = 1)
        {
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
                var group = await _studentGroupService.GetByTopicAsync(t.Id);

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

                var reviewedByMe = currentUserId.HasValue && submission != null ?
                        await _reviewLogService.IsUserReviewed(currentUserId.Value, submission.Id) : false;
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
