using EcommerceApp.Enum;

namespace EcommerceApp.Model
{
    public class User: Base
    {
       public UserRole Role { get; set; } = UserRole.Customer;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }= string.Empty;
        public string? PhoneNumber { get; set; }

        //Navigation
        public ICollection<Order>? Orders { get; set; }
        public ICollection<Address>? Addresses { get; set; }

    }
}
