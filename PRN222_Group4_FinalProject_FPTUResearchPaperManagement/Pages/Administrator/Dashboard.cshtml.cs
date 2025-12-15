using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace PRN222_Group4_FinalProject_FPTUResearchPaperManagement.Pages.Administrator
{
    public class DashboardModel : PageModel
    {
        public IActionResult OnGet()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // if (userId == null)
            // {
            //     return RedirectToPage("/Authentication/Login");
            // }

            return Page();
        }
    }
}
