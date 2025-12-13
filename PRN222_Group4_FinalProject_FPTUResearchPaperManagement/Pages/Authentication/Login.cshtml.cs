using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Dtos;
using Service.Interfaces;
using System.Security.Claims;

namespace PRN222_Group4_FinalProject_FPTUResearchPaperManagement.Pages.Authentication
{
    public class LoginModel : PageModel
    {
        private readonly IAuthService _authService;

        public LoginModel(IAuthService authService)
        {
            _authService = authService;
        }

        [BindProperty]
        public LoginRequest Input { get; set; }

        public string ErrorMessage { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            string? token = await _authService.LoginAsync(Input.Email, Input.Password);

            if (token == null)
            {
                ErrorMessage = "Invalid email or password";
                return Page();
            }

            // Store token in cookie
            Response.Cookies.Append("jwt", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTime.UtcNow.AddHours(2),
                SameSite = SameSiteMode.Strict
            });

            // Extract role from JWT
            var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            return roleClaim switch
            {
                "Administrator" => RedirectToPage("/Administrator/Dashboard"),
                "Student" => RedirectToPage("/Student/Topic"),
                "GraduationProjectEvaluationCommitteeMember" => RedirectToPage("/Committee/Dashboard"),
                "Instructor" => RedirectToPage("/Instructor/Dashboard"),
                _ => RedirectToPage("/Index")
            };
        }
    }
}
