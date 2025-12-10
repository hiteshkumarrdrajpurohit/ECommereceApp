using EcommerceApp.Enum;
using EcommerceApp.Model;

namespace EcommerceApp.DTO
{
    public class OrderDTO
    {
        public string Remarks { get; set; } = string.Empty;

        public  PaymentMethod Method { get; set; }

        public int ShippingAddressId { get; set; }

        public string RazorPageTransactionID {  get; set; } = string.Empty;
    }

   
}
