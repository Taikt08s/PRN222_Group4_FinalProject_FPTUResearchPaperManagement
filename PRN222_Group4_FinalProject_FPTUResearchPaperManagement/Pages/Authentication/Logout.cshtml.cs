using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PRN222_Group4_FinalProject_FPTUResearchPaperManagement.Pages.Authentication
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            Response.Cookies.Append("jwt", "", new CookieOptions
            {
                MaxAge = TimeSpan.Zero,
                HttpOnly = true,
                SameSite = SameSiteMode.Strict
            });
            return RedirectToPage("/Authentication/Login");
        }
    }
}
