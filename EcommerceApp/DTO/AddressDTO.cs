using System.ComponentModel.DataAnnotations;

namespace EcommerceApp.DTO
{
    public class AddressDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;
         
        [Required]
        public string Phone {  get; set; } =string.Empty;
        [Required]
        public string Street { get; set; } = string.Empty;
        
        [Required] 
        public string City { get; set; } = string.Empty;
        
        [Required] 
        public string State { get; set; } = string.Empty;
  
        [Required] 
        public string PostalCode { get; set; } = string.Empty;
        
        [Required] 
        public string Country { get; set; } = string.Empty;
    }
}
