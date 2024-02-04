using Microsoft.AspNetCore.Identity;

namespace AppSecAssignment2RE.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string CreditCardNumber { get; set; }
        public string Gender { get; set; }
        public string MobileNumber { get; set; }
        public string DeliveryAddress { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public byte[]? ProfilePhoto { get; set; }

        public string AboutMe { get; set; }

        public new string Email { get; set; }

        public byte[] Key { get; set; }
        public byte[] IV { get; set; }
    }
}
