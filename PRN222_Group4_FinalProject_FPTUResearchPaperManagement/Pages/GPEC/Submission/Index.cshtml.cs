using BusinessObject.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;

namespace PRN222_Group4_FinalProject_FPTUResearchPaperManagement.Pages.GPEC.Submission
{
    [Authorize(Roles = "GraduationProjectEvaluationCommitteeMember")]
    public class IndexModel : PageModel
    {
        private ISubmissionService _submissionService { get; }
        private IReviewLogService _reviewLogService { get; }

        public IndexModel(ISubmissionService submissionService, IReviewLogService reviewLogService)
        {
            _reviewLogService = reviewLogService;
            _submissionService = submissionService;
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

        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalPages { get; set; }


        public List<SubmissionListItem> Submissions { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int pageIndex = 1)
        {
            PageIndex = pageIndex;

            var userIdStr = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            Guid? currentUserId = Guid.TryParse(userIdStr, out var g) ? g : null;
            if (currentUserId == null)
            {
                return Redirect("Authorization/login");
            }

            SubmissionFilter filter = new()
            {
                SubmissionStatuses = new List<string>() { "Submitted", "Reviewing", "Suspended", "Rejected", "Approved" }
            };

            var totalCount = await _submissionService.CountFilteredAsync(filter);
            TotalPages = (int)Math.Ceiling(totalCount / (double)PageSize);

            var submissions = await _submissionService.GetPaginationAsync(filter, PageIndex, PageSize);
            var myReviewedSubmissionIds = await _reviewLogService.GetSubmissionUserReviewed(currentUserId.Value, submissions.Select(sub => sub.Id).ToList());


            Submissions = (await Task.WhenAll(
            submissions.Select(async s => new SubmissionListItem
            {
                SubmissionId = s.Id,
                TopicTitle = s.Topic?.Title ?? "",
                GroupId = s.Group_Id,
                MemberCount = s.Group?.Members?.Count ?? 0,
                Status = s.Status,
                PlagiarismFlag = s.Plagiarism_Flag,
                ReviewedByMe = await _reviewLogService
                    .IsUserReviewed(currentUserId.Value, s.Id)
            }))).ToList();
            return Page();

        }


    }
}
