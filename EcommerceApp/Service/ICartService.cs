using EcommerceApp.Model;

namespace EcommerceApp.Service
{
    public interface ICartService
    {
        Task<Cart> GetOrCreateCartAsync(int userId);
        Task<Cart> GetCartAsync(int userId);
        Task AddToCartAsync(int userId, int itemId, int quantity);
        Task RemoveCartItemAsync(int cartItemId);
        Task ClearCartAsync(int userId);
        Task<decimal> CalculateTotalAsync(int cartId);
    }
}
