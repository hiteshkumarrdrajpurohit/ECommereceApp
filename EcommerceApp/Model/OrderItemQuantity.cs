using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EcommerceApp.Model
{
    public class OrderItemQuantity : Base
    {
     

        [Required]                                  
        public int OrderId { get; set; }

        [JsonIgnore]
        public Order? Order { get; set; }

        [Required]

         public int ItemId { get; set; }

        [JsonIgnore]
        public Item? Item { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
