using AppSecAssignment2RE.ViewModels;
using AppSecAssignment2RE.Models;
using AppSecAssignment2RE.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Logging;
using Newtonsoft.Json;


namespace AppSecAssignment2RE.Pages
{
    public class LoginModel : PageModel
    {

        

        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;

        private readonly AuditLogService auditLogService;

        public LoginModel(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, AuditLogService auditLogService)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.auditLogService = auditLogService;
        }

        [BindProperty]
        public Login LModel { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var recaptchaResponse = HttpContext.Request.Form["g-recaptcha-response"];

            var recaptchaClient = new HttpClient();
            var recaptchaResult = await recaptchaClient.GetStringAsync($"https://www.google.com/recaptcha/api/siteverify?secret=6Ld4ZWQpAAAAABabbcPEkiVUoko3SX8Od9OtQRB_&response={recaptchaResponse}");
            var recaptchaData = JsonConvert.DeserializeObject<Recaptcha>(recaptchaResult);

            if (!recaptchaData.Success)
            {
                ModelState.AddModelError(string.Empty, "reCAPTCHA verification failed. Please try again.");
                return Page();
            }



            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(LModel.Email, LModel.Password, LModel.RememberMe, lockoutOnFailure: true);

                if (result.Succeeded)
                {
                    HttpContext.Session.SetString("Username", LModel.Email);
                    SessionManager.AddSession(LModel.Email, HttpContext.Session.Id);

                    auditLogService.Log(LModel.Email, "Logging In", "User has logged in");


                    return RedirectToPage("Index");
                }
                else if (result.IsLockedOut)
                {
                    ModelState.AddModelError("LModel.Email", "Account is locked out. Please try again later.");
                    auditLogService.Log(LModel.Email, "Logging In", "User has been locked out");

                }
                else if (!result.Succeeded)
                {
                    var user = await userManager.FindByEmailAsync(LModel.Email);
                    if (user != null)
                    {
                        user.AccessFailedCount++;

                        if (user.AccessFailedCount >= userManager.Options.Lockout.MaxFailedAccessAttempts)
                        {
                            user.LockoutEnd = DateTimeOffset.UtcNow.Add(userManager.Options.Lockout.DefaultLockoutTimeSpan);
                        }

                        await userManager.UpdateAsync(user);
                    }

                    ModelState.AddModelError("LModel.Email", "Email or password is incorrect.");

                }
            }

            return Page();
        }
    }
}
