using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace AppSecAssignment2RE.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [IgnoreAntiforgeryToken]
    public class ErrorModel : PageModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        private readonly ILogger<ErrorModel> _logger;

        public ErrorModel(ILogger<ErrorModel> logger)
        {
            _logger = logger;
        }

        public string ErrorMessage { get; private set; }
        public int StatusCode { get; private set; }

        
        public IActionResult OnGet(int statusCode)
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            StatusCode = statusCode;
            ErrorMessage = $"Error {statusCode}: {GetDefaultMessage(statusCode)}";
            return Page();
        }
        private string GetDefaultMessage(int statusCode)
        {
            return statusCode switch
            {
                404 => "The resource you requested could not be found.",
                403 => "You are not authorized to access this resource.",
                _ => "An error occurred."
            };
        }
    }
}