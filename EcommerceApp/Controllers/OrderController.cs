using EcommerceApp.DTO;
using EcommerceApp.Enum;
using EcommerceApp.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var cart = _dbContext.Carts.FirstOrDefault( c => Convert.ToInt32(c.UserId) == Convert.ToInt32(userId) );

            if (cart ==null) return Unauthorized();

            var order = new Order
            {
                UserId=Convert.ToInt32(userId),
                TotalAmount = cart.TotalAmount,
                Status = OrderStatus.Pending,
                ShippingAddressId =dto.ShippingAddressId
            };

            var transaction = new Transaction
            {
                Order = order,
                RazorPageTransactionID=dto.RazorPageTransactionID,
                Method=dto.Method,
                Remarks = dto.Remarks,
            };

            _dbContext.Orders.Add(order);
            _dbContext.Transactions.Add(transaction);

            _dbContext.SaveChanges();

            return Ok("Oder Created Succesfully ");
        }
    }
}
