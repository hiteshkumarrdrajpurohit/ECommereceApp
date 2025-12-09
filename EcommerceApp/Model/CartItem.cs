using System.Text.Json.Serialization;

namespace EcommerceApp.Model
{
    public class CartItem : Base
    {
     

        public int CartId { get; set; }

        [JsonIgnore]
        public Cart? Cart { get; set; }

        public int ItemId { get; set; }
        public Item? Item { get; set; }

        public int Quantity { get; set; }
     
    }
}
