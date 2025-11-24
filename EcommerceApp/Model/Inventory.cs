using EcommerceApp.Enum;

namespace EcommerceApp.Model
{
    public class Inventory : Base
    {
        public int ItemId { get; set; }
        public Item? Item { get; set; }

        public int QuantityAvailable { get; set; }
        public int QuantityReserved { get; set; }
        public int QuantitySold { get; set; }

        public int LowStockThreshold { get; set; } = 5;
        public StockStatus Status { get; set; } = StockStatus.InStock;
    }
}
