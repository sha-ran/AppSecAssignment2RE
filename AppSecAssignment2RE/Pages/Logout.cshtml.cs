using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AppSecAssignment2RE.Models;


namespace AppSecAssignment2RE.Pages
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        public LogoutModel(SignInManager<ApplicationUser> signInManager)
        {
            this.signInManager = signInManager;
        }
        public void OnGet() { }

        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await signInManager.SignOutAsync();

            //var sessionId = HttpContext.Session.Id;

            // Clear session from the database
            //_contextAccessor.ClearSession(sessionId);

            string userId = HttpContext.Session.GetString("Username");
            SessionManager.RemoveSession(userId);
            // Clear session data
            HttpContext.Session.Clear();

            return RedirectToPage("Login");
        }
        public async Task<IActionResult> OnPostDontLogoutAsync()
        {
            return RedirectToPage("Index");
        }
    }
}
