using EcommerceApp.Enum;

namespace EcommerceApp.Model
{
    public class Item : Base
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public ItemCategory Category { get; set; } = ItemCategory.Electronics;

        public int InventoryId { get; set; }
        public Inventory? Inventory { get; set; }
        
    }
}
