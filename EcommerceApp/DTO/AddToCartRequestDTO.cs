namespace EcommerceApp.DTO
{
    public class AddToCartRequestDTO
    {
        public int UserId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
    }

}
