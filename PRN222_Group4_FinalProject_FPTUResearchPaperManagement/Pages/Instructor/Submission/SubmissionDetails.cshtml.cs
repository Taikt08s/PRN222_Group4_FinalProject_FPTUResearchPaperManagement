using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Dtos;
using Service.Interfaces;

namespace PRN222_Group4_FinalProject_FPTUResearchPaperManagement.Pages.Instructor.Submission
{
    [Authorize(Roles = "Instructor,GraduationProjectEvaluationCommitteeMember")]
    public class SubmissionDetailsModel : PageModel
    {
        private readonly ISubmissionService _submissionService;

        public SubmissionDetailsModel(ISubmissionService submissionService)
        {
            _submissionService = submissionService;
        }

        public SubmissionDto? Submission { get; set; }

        public List<SubmissionFileDto> Files { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public int SubmissionId { get; set; }

        public async Task<IActionResult> OnGetAsync(int submissionId)
        {
            if (submissionId <= 0)
                return BadRequest();

            SubmissionId = submissionId;

            Submission = await _submissionService.GetByIdAsync(submissionId);

            if (Submission == null)
                return NotFound();

            Files = await _submissionService.GetFilesAsync(submissionId);

            ViewData["ShowSidebar"] = true;
            ViewData["ActiveMenu"] = "TopicSubmissions";

            return Page();
        }
    }
}