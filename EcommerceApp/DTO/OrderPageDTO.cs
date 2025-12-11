using EcommerceApp.Enum;
using System.ComponentModel.DataAnnotations;

namespace EcommerceApp.DTO
{
    public class OrderPageDTO
    {
        public int Id { get; set; }
        public string Remarks { get; set; } = string.Empty;

        public PaymentMethod Method { get; set; }

        public int ShippingAddressId { get; set; }

        public OrderStatus Status { get; set; }
        public string RazorPageTransactionID { get; set; } = string.Empty;
   
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Phone { get; set; } = string.Empty;
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
