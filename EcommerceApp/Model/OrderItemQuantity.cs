using System.ComponentModel.DataAnnotations;

namespace EcommerceApp.Model
{
    public class OrderItemQuantity : Base
    {
     

        [Required]                                  
        public int OrderId { get; set; }
        public Order? Order { get; set; }

        [Required]
         public int ItemId { get; set; }
        
        public Item? Item { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
