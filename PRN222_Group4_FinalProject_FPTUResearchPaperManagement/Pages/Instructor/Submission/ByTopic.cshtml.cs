using System.Linq;
using Microsoft.AspNetCore.Authorization;
using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Service.Dtos;
using Service.Interfaces;

namespace PRN222_Group4_FinalProject_FPTUResearchPaperManagement.Pages.Instructor.Submission
{
    [Authorize(Roles = "Instructor,GraduationProjectEvaluationCommitteeMember")]
    public class ByTopicModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly ITopicService _topicService;
        private readonly ISubmissionService _submissionService;

        public ByTopicModel(AppDbContext context, ITopicService topicService, ISubmissionService submissionService)
        {
            _context = context;
            _topicService = topicService;
            _submissionService = submissionService;
        }

        public TopicResponseModel Topic { get; set; }
        public List<GroupSubmissionViewModel> Groups { get; set; } = new();

        public class GroupSubmissionViewModel
        {
            public int GroupId { get; set; }
            public string LeaderName { get; set; }
            public int MemberCount { get; set; }
            public int? SubmissionId { get; set; }
            public string SubmissionStatus { get; set; }
            public List<MemberInfo> Members { get; set; } = new();
        }

        public class MemberInfo
        {
            public Guid Id { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }
            public bool IsLeader { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            ViewData["ShowSidebar"] = true;
            ViewData["ActiveMenu"] = "TopicSubmissions";

            Topic = await _topicService.GetTopicByIdAsync(id);
            if (Topic == null) return NotFound();

            // load groups for topic with members (include student data)
            var groups = await _context.StudentGroups
                .Where(g => g.Topic_Id == id)
                .Include(g => g.Members)
                    .ThenInclude(m => m.Student)
                .ToListAsync();

            // ensure we have a semester id; if Topic.SemesterId is 0, fallback to DB
            var semesterId = Topic.SemesterId;
            if (semesterId == 0)
            {
                var topicEntity = await _context.Topics
                    .Where(t => t.Id == id)
                    .Select(t => new { t.Semester_Id })
                    .FirstOrDefaultAsync();
                if (topicEntity != null) semesterId = topicEntity.Semester_Id;
            }

            foreach (var g in groups)
            {
                var leader = g.Members.FirstOrDefault(m => m.Is_Leader)?.Student?.Full_Name ?? "N/A";
                var memberCount = g.Members.Count;

                var submissionDto = await _submissionService.GetSubmissionAsync(Topic.Id, g.Id, semesterId);

                var vm = new GroupSubmissionViewModel
                {
                    GroupId = g.Id,
                    LeaderName = leader,
                    MemberCount = memberCount,
                    SubmissionId = submissionDto?.Id,
                    SubmissionStatus = submissionDto?.Status ?? "Not submitted",
                    Members = g.Members.Select(m => new MemberInfo
                    {
                        Id = m.Student_Id,
                        FullName = m.Student?.Full_Name ?? "",
                        Email = m.Student?.Email ?? "",
                        IsLeader = m.Is_Leader
                    }).ToList()
                };

                Groups.Add(vm);
            }

            return Page();
        }
    }
}