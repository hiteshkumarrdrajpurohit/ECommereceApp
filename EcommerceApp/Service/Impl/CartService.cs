using EcommerceApp;
using EcommerceApp.Model;
using EcommerceApp.Service;
using Microsoft.EntityFrameworkCore;
namespace EcommerceApp.Service.Impl
{
    public class CartService : ICartService
    {
        private readonly ApplicationDbContext _db;

        public CartService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Cart?> GetCartAsync(int userId)
        {
            return await _db.Carts
                .Include(c => c.Items)
                .ThenInclude(ci => ci.Item)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task<Cart> GetOrCreateCartAsync(int userId)
        {
            var cart = await GetCartAsync(userId);

            if (cart != null)
                return cart;

            // Lazy create when adding first item
            cart = new Cart
            {
                UserId = userId,
                TotalAmount = 0
            };

            _db.Carts.Add(cart);
            await _db.SaveChangesAsync();
            return cart;
        }

        public async Task AddToCartAsync(int userId, int itemId, int quantity)
        {
            var cart = await GetOrCreateCartAsync(userId);

            var existingItem = cart.Items.FirstOrDefault(ci => ci.ItemId == itemId);

            var item = await _db.Items.FindAsync(itemId);
            if (item == null)
                throw new Exception("Item not found");

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                cart.Items.Add(new CartItem
                {
                    ItemId = itemId,
                    Item = item,
                    Quantity = quantity,
             
                });
            }

            cart.TotalAmount = cart.Items.Sum(ci => ci.Quantity * ci.Item.Price);

            await _db.SaveChangesAsync();
        }

        public async Task RemoveCartItemAsync(int cartItemId)
        {
            var item = await _db.CartItems.FindAsync(cartItemId);

            if (item != null)
            {
                _db.CartItems.Remove(item);

                await _db.SaveChangesAsync();
            }
        }

        public async Task ClearCartAsync(int userId)
        {
            var cart = await GetCartAsync(userId);

            if (cart != null)
            {
                _db.CartItems.RemoveRange(cart.Items);
                cart.TotalAmount = 0;

                await _db.SaveChangesAsync();
            }
        }

        public async Task<decimal> CalculateTotalAsync(int cartId)
        {
            var cart = await _db.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.Id == cartId);

            if (cart == null)
                return 0;

            cart.TotalAmount = cart.Items.Sum(ci => ci.Quantity * ci.Item.Price);
            await _db.SaveChangesAsync();

            return cart.TotalAmount;
        }
    }
}
