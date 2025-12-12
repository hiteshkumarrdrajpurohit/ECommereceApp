using EcommerceApp.Enum;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EcommerceApp.Model
{
    public class User: Base
    {
        [Required]
       public UserRole Role { get; set; } = UserRole.Customer;
        [Required] 
        
        public string FirstName { get; set; }= string.Empty;
        [Required]
        public string LastName { get; set; }= string.Empty;
        [Required]
        public string Email { get; set; }= string.Empty;
        [Required]
        public string PasswordHash { get; set; }= string.Empty;
        [Required]
        public string? PhoneNumber { get; set; }

        //Navigation
        public ICollection<Order>? Orders { get; set; }

        public ICollection<Address>? Addresses { get; set; } = new List<Address>();

        [JsonIgnore]
        public Cart? Cart { get; set; }
    }
}
