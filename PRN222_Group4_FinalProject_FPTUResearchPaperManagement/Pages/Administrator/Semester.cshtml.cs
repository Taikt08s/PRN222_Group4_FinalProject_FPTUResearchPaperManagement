using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Interfaces;

namespace PRN222_Group4_FinalProject_FPTUResearchPaperManagement.Pages.Administrator
{
    public class SemesterModel : PageModel
    {
        private readonly ISemesterService _semesterService;

        public SemesterModel(ISemesterService semesterService)
        {
            _semesterService = semesterService;
        }

        public IReadOnlyList<string> TermOptions { get; } = new[] { "Spring", "Summer", "Fall", "Special" };

        public List<Semester> ExistingSemesters { get; private set; } = new();

        [BindProperty]
        public SemesterForm SemesterInput { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadSemestersAsync();
            EnsureDefaults();
            return Page();
        }

        public async Task<IActionResult> OnPostCreateSemesterAsync()
        {
            await LoadSemestersAsync();

            if (SemesterInput.StartDate >= SemesterInput.EndDate)
            {
                ModelState.AddModelError("SemesterInput.EndDate", "Ngày kết thúc phải sau ngày bắt đầu.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var semester = new Semester
            {
                Term = SemesterInput.Term.Trim(),
                Year = SemesterInput.Year,
                Start_Date = SemesterInput.StartDate,
                End_Date = SemesterInput.EndDate
            };

            try
            {
                await _semesterService.CreateSemesterAsync(semester);
                TempData["Message"] = "Đã tạo kỳ học mới.";
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }
        }

        private async Task LoadSemestersAsync()
        {
            var semesters = await _semesterService.GetAllAsync();
            ExistingSemesters = semesters
                .OrderByDescending(s => s.Start_Date)
                .ToList();
        }

        private void EnsureDefaults()
        {
            if (SemesterInput.Year == 0)
            {
                SemesterInput.Year = DateTime.Today.Year;
            }

            if (SemesterInput.StartDate == default)
            {
                SemesterInput.StartDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            }

            if (SemesterInput.EndDate == default)
            {
                SemesterInput.EndDate = SemesterInput.StartDate.AddMonths(4);
            }
        }

        public class SemesterForm
        {
            [Range(2020, 2100, ErrorMessage = "Năm không hợp lệ."), Display(Name = "Năm học")]
            public int Year { get; set; } = DateTime.Today.Year;

            [Required(ErrorMessage = "Vui lòng chọn học kỳ."), Display(Name = "Học kỳ")]
            public string Term { get; set; } = "Spring";

            [Required(ErrorMessage = "Vui lòng chọn ngày bắt đầu."), Display(Name = "Ngày bắt đầu"), DataType(DataType.Date)]
            public DateTime StartDate { get; set; } = DateTime.Today;

            [Required(ErrorMessage = "Vui lòng chọn ngày kết thúc."), Display(Name = "Ngày kết thúc"), DataType(DataType.Date)]
            public DateTime EndDate { get; set; } = DateTime.Today.AddMonths(4);
        }
    }
}
