using System.Text.Json.Serialization;

namespace EcommerceApp.Model
{
    public class Cart : Base
    {
        public int UserId { get; set; }
        [JsonIgnore]
        public User? User { get; set; }

        public ICollection<CartItem> Items { get; set; } = new List<CartItem>();

        public decimal TotalAmount { get; set; }
    }
}
