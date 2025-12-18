using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BusinessObject.Enums;
using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Dtos;
using Service.Interfaces;

namespace PRN222_Group4_FinalProject_FPTUResearchPaperManagement.Pages.Administrator
{
    public class TopicModel : PageModel
    {
        private const int TopicPageSize = 50;

        private readonly ITopicService _topicService;
        private readonly ISemesterService _semesterService;
        private readonly IUserService _userService;

        public TopicModel(ITopicService topicService, ISemesterService semesterService, IUserService userService)
        {
            _topicService = topicService;
            _semesterService = semesterService;
            _userService = userService;
        }

        public IReadOnlyList<AccountMajor> MajorOptions { get; private set; } = Array.Empty<AccountMajor>();

        public IReadOnlyList<TopicStatus> StatusOptions { get; private set; } = Array.Empty<TopicStatus>();

        public List<Semester> Semesters { get; private set; } = new();

        public List<TopicResponseModel> Topics { get; private set; } = new();

        public List<UserAdminDto> InstructorOptions { get; private set; } = new();

        public int TotalTopics { get; private set; }

        public int? EditingTopicId { get; private set; }

        [BindProperty]
        public TopicForm TopicInput { get; set; } = new();

        [BindProperty]
        public TopicForm UpdateInput { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int? editId)
        {
            await LoadReferenceDataAsync();
            await LoadTopicsAsync();
            EnsureDefaultSelections();

            if (editId.HasValue)
            {
                await PrepareUpdateAsync(editId.Value);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostCreateTopicAsync()
        {
            foreach (var key in ModelState.Keys.Where(k => k.StartsWith("UpdateInput")).ToList())
            {
                Console.WriteLine("Removing ModelState key: " + key);
                ModelState.Remove(key);
            }

            await LoadReferenceDataAsync();
            await LoadTopicsAsync();

            ValidateSemesterSelection(TopicInput.SemesterId, "TopicInput.SemesterId");
            ValidateInstructorSelection(TopicInput.CreatedBy, "TopicInput.CreatedBy");

            Console.WriteLine("TopicInput.DeadlineDate: " + TopicInput.DeadlineDate);
            if (TopicInput.DeadlineDate == default)
            {
                ModelState.AddModelError("TopicInput.DeadlineDate", "Vui lòng chọn hạn nộp rõ ràng.");
            }

           
            var creatorIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(creatorIdValue))
            {
                return RedirectToPage("/Authentication/Login");
            }
            var request = new CreateTopicRequest
            {
                TopicName = TopicInput.TopicName!.Trim(),
                TopicDescription = TopicInput.TopicDescription?.Trim() ?? string.Empty,
                SubmissionInstruction = TopicInput.SubmissionInstruction?.Trim() ?? string.Empty,
                InstructorId = TopicInput.CreatedBy!.Value,
                CreatedBy = Guid.Parse(creatorIdValue),
                Major = TopicInput.Major,
                SemesterId = TopicInput.SemesterId,
                IsGroupTopic = TopicInput.IsGroupTopic,
                DeadlineDate = TopicInput.DeadlineDate,
                Status = TopicInput.Status
            };

            // if (!ModelState.IsValid)
            // {
            //     Console.WriteLine("ModelState is invalid. Errors:");
            //     foreach (var modelState in ModelState.Values)
            //     {
            //         foreach (var error in modelState.Errors)
            //         {
            //             Console.WriteLine($"  - {error.ErrorMessage}");
            //         }
            //     }
            //     return Page();
            // }

            Console.WriteLine("Creating topic with CreatedBy: " + request.CreatedBy);
            try
            {
                await _topicService.CreateTopicAsync(request);
                TempData["Message"] = "Đã tạo đề tài mới thành công.";
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }
        }

        public async Task<IActionResult> OnPostUpdateTopicAsync()
        {
            foreach (var key in ModelState.Keys.Where(k => k.StartsWith("TopicInput")).ToList())
            {
                ModelState.Remove(key);
            }

            await LoadReferenceDataAsync();
            await LoadTopicsAsync();

            EditingTopicId = UpdateInput.TopicId;

            if (UpdateInput.TopicId <= 0)
            {
                ModelState.AddModelError(string.Empty, "Đề tài không hợp lệ.");
            }

            ValidateSemesterSelection(UpdateInput.SemesterId, "UpdateInput.SemesterId");
            ValidateInstructorSelection(UpdateInput.CreatedBy, "UpdateInput.CreatedBy");

            if (UpdateInput.DeadlineDate == default)
            {
                ModelState.AddModelError("UpdateInput.DeadlineDate", "Vui lòng chọn hạn nộp rõ ràng.");
            }

            var editorIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(editorIdValue))
            {
                return RedirectToPage("/Authentication/Login");
            }

            var request = new UpdateTopicRequest
            {
                TopicId = UpdateInput.TopicId,
                TopicName = UpdateInput.TopicName!.Trim(),
                TopicDescription = UpdateInput.TopicDescription?.Trim() ?? string.Empty,
                SubmissionInstruction = UpdateInput.SubmissionInstruction?.Trim() ?? string.Empty,
                SemesterId = UpdateInput.SemesterId,
                InstructorId = UpdateInput.CreatedBy!.Value,
                CreatedBy = Guid.Parse(editorIdValue),
                IsGroupTopic = UpdateInput.IsGroupTopic,
                DeadlineDate = UpdateInput.DeadlineDate,
                Status = UpdateInput.Status,
                Major = UpdateInput.Major
            };

            try
            {
                await _topicService.UpdateTopicAsync(request);
                TempData["Message"] = "Đã cập nhật đề tài thành công.";
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }
        }

        private async Task LoadReferenceDataAsync()
        {
            MajorOptions = Enum.GetValues<AccountMajor>();
            StatusOptions = Enum.GetValues<TopicStatus>();
            var semesters = await _semesterService.GetAllAsync();
            Semesters = semesters
                .OrderByDescending(s => s.Start_Date)
                .ToList();

            var users = await _userService.GetAllAsync();
            Console.WriteLine("Total users loaded: " + users.Count);
            var instructorRole = AccountRole.Instructor.ToString();
            InstructorOptions = users
                .Where(u => string.Equals(u.Role, instructorRole, StringComparison.OrdinalIgnoreCase))
                .OrderBy(u => u.FullName, StringComparer.OrdinalIgnoreCase)
                .ToList();
        }

        private async Task LoadTopicsAsync()
        {
            Console.WriteLine("Loading topics...");
            TotalTopics = await _topicService.CountAsync(null);

            Console.WriteLine("Loading total topics: " + TotalTopics);
            Topics = await _topicService.GetPaginationAsync(null, 1, TopicPageSize);
            Console.WriteLine("Loaded topics count: " + Topics.Count);
        }

        private async Task PrepareUpdateAsync(int topicId)
        {
            var topic = await _topicService.GetTopicByIdAsync(topicId);
            if (topic == null)
            {
                TempData["Code"] = "NOT_FOUND";
                TempData["Message"] = "Không tìm thấy đề tài.";
                return;
            }

            EditingTopicId = topicId;

            var updateModel = new TopicForm
            {
                TopicId = topic.Id,
                TopicName = topic.Title,
                TopicDescription = topic.Description,
                SubmissionInstruction = topic.SubmissionInstruction,
                SemesterId = topic.SemesterId,
                CreatedBy = topic.CreatedBy,
                DeadlineDate = topic.Deadline_Date,
                IsGroupTopic = topic.Is_Group_Required
            };

            if (Enum.TryParse<AccountMajor>(topic.Major, out var parsedMajor))
            {
                updateModel.Major = parsedMajor;
            }

            if (Enum.TryParse<TopicStatus>(topic.Status, out var parsedStatus))
            {
                updateModel.Status = parsedStatus;
            }

            if (updateModel.SemesterId == 0 && Semesters.Any())
            {
                updateModel.SemesterId = Semesters.First().Id;
            }

            UpdateInput = updateModel;
        }

        private void EnsureDefaultSelections()
        {
            if (TopicInput.DeadlineDate == default)
            {
                TopicInput.DeadlineDate = DateTime.Today.AddDays(14);
            }

            if (TopicInput.SemesterId == 0 && Semesters.Any())
            {
                TopicInput.SemesterId = Semesters.First().Id;
            }

            if (!TopicInput.CreatedBy.HasValue && InstructorOptions.Any())
            {
                TopicInput.CreatedBy = InstructorOptions.First().Id;
            }
        }

        private void ValidateSemesterSelection(int semesterId, string fieldName)
        {
            if (!Semesters.Any())
            {
                ModelState.AddModelError(string.Empty, "Chưa có kỳ học để gán đề tài.");
                return;
            }

            if (!Semesters.Any(s => s.Id == semesterId))
            {
                ModelState.AddModelError(fieldName, "Kỳ học được chọn không hợp lệ.");
            }
        }

        private void ValidateInstructorSelection(Guid? CreatedBy, string fieldName)
        {
            if (!InstructorOptions.Any())
            {
                ModelState.AddModelError(fieldName, "Chưa có giảng viên khả dụng để gán đề tài.");
                return;
            }

            if (!CreatedBy.HasValue)
            {
                ModelState.AddModelError(fieldName, "Vui lòng chọn giảng viên phụ trách.");
                return;
            }

            if (!InstructorOptions.Any(i => i.Id == CreatedBy.Value))
            {
                ModelState.AddModelError(fieldName, "Giảng viên được chọn không hợp lệ.");
            }
        }

        public class TopicForm
        {
            public int TopicId { get; set; }

            [Required(ErrorMessage = "Tên đề tài là bắt buộc."), StringLength(200, ErrorMessage = "Tên đề tài tối đa 200 ký tự.")]
            [Display(Name = "Tên đề tài")]
            public string? TopicName { get; set; }

            [Display(Name = "Mô tả"), StringLength(1200, ErrorMessage = "Mô tả tối đa 1200 ký tự.")]
            public string? TopicDescription { get; set; }

            [Display(Name = "Hướng dẫn nộp"), StringLength(1500, ErrorMessage = "Hướng dẫn tối đa 1500 ký tự.")]
            public string? SubmissionInstruction { get; set; }

            [Display(Name = "Kỳ học"), Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn kỳ học.")]
            public int SemesterId { get; set; }

            [Display(Name = "Giảng viên phụ trách")]
            public Guid? CreatedBy { get; set; }

            [Required(ErrorMessage = "Vui lòng chọn hạn đăng ký."), Display(Name = "Hạn đăng ký")]
            public DateTime DeadlineDate { get; set; } = DateTime.Today.AddDays(14);

            [Display(Name = "Chuyên ngành")]
            public AccountMajor Major { get; set; } = AccountMajor.SoftwareEngineering;

            [Display(Name = "Trạng thái")]
            public TopicStatus Status { get; set; } = TopicStatus.Created;

            [Display(Name = "Yêu cầu lập nhóm")]
            public bool IsGroupTopic { get; set; }
        }
    }
}
