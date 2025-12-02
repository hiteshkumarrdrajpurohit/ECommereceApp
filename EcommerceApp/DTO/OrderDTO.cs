using EcommerceApp.Model;

namespace EcommerceApp.DTO
{
    public class OrderDTO
    {

        public decimal TotalAmount { get; set; }
        public IDictionary<int,int>? ItemIdQuantity { get; set; }

        public int ShippingAddressId { get; set; }
    }

   
}
