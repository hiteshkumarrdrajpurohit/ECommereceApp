using EcommerceApp.DTO;
using EcommerceApp.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        public OrderController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("create")]
        public IActionResult CreateOrder([FromBody] OrderDTO dto)
        {
            var userId = HttpContext.Items["UserId"]?.ToString();
            var orderItems = new List<OrderItemQuantity>();

            if (userId == null) return Unauthorized();

            var user = _dbContext.Users.FirstOrDefault(u => u.Id.ToString() == userId && u.IsActive == true);

            if (user == null) return Unauthorized();

            var order = new Order
            {
                UserInfo = user,
                ShippingAddressId = dto.ShippingAddressId,
                TotalAmount = dto.TotalAmount,
                OrderItemQuantity = orderItems,
            };

            if (dto.ItemIdQuantity !=null)
            {

                foreach (var item in dto.ItemIdQuantity)
                {
                    var orderItem = new OrderItemQuantity
                    {
                        Order = order,
                        ItemId = item.Key,
                        Quantity = item.Value
                    };
                }
            }

            return Ok();
        }
    }
}
