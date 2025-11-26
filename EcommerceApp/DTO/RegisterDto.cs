using EcommerceApp.Enum;
using System.ComponentModel.DataAnnotations;

namespace EcommerceApp.DTO
{
    public class RegisterDto
    {
        [Required]
        public UserRole Role { get; set; }
        [Required]
        public string FirstName { get; set; } = "";

        [Required]
        public string LastName { get; set; } = "";

        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";

        [Required]
      
        public string Password { get; set; } = "";

        [Required]

        public string PhoneNumber { get; set; } = "";
    }
}
