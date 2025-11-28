using EcommerceApp.Enum;

namespace EcommerceApp.DTO
{
    public class InventoryDto
    {
        public int ItemId { get; set; }
        public int QuantityAvailable { get; set; }
        public int QuantityReserved { get; set; }
        public int QuantitySold { get; set; }

        public int LowStockThreshold { get; set; } = 5;
        public StockStatus Status { get; set; } = StockStatus.InStock;
    }
}
