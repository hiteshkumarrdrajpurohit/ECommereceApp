using EcommerceApp.DTO;
using EcommerceApp.Enum;
using EcommerceApp.Model;
using EcommerceApp.Shared.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
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

            var shippingAddress = _dbContext.Addresses.FirstOrDefault( c => c.Id == dto.ShippingAddressId);

            if (cart ==null) return Unauthorized();

            var order = new Order
            {
                UserId=Convert.ToInt32(userId),
                TotalAmount = cart.TotalAmount,
                Status = OrderStatus.Pending,
               ShippingAddressId =dto.ShippingAddressId,
                ShippingAddress = shippingAddress
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

        [HttpGet()]

        public IActionResult GetOrderByOrderId( [FromQuery] int  id)
        {
           

            var order = _dbContext
                .Orders
                .Include(o => o.ShippingAddress)
                .Include(o => o.Transaction)
                .FirstOrDefault(o => o.Id ==id );

            if (order == null) return NotFound("Order not found.");

            if (order.ShippingAddress == null)
                return NotFound("Shipping address not found for this order.");

            if (order.Transaction == null)
                return NotFound("Transaction not found for this order.");

            var orderResponse = new OrderPageDTO
            {
                Id = order.Id,
                Remarks = order.Transaction.Remarks,
                Method = order.Transaction.Method,
                Status = order.Status,
                ShippingAddressId = order.ShippingAddressId,
                RazorPageTransactionID = order.Transaction.RazorPageTransactionID,
                Name = order.ShippingAddress.Name,
                Phone = order.ShippingAddress.Phone,
                Street = order.ShippingAddress.Street,
                City = order.ShippingAddress.City,
                State = order.ShippingAddress.State,
                PostalCode = order.ShippingAddress.PostalCode,
                Country = order.ShippingAddress.Country
            };
           

            return Ok(orderResponse);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllOrders([FromQuery] PagingParameters paging) {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null) return Unauthorized("Invalid user");

            var query = _dbContext
                        .Orders
                        .Where(o => o.UserId == int.Parse(userId))
                        .AsNoTracking()
                        .Include( o => o.ShippingAddress)
                        .Include( o => o.Transaction)
                        .OrderBy(o => o.Id);

            var pagedResult = await query
         .ToPagedResponseAsync(
             paging.Page,
             paging.PageSize,
             o => new OrderPageDTO
             {
                 Id = o.Id,
                 Remarks = o.Transaction != null ? o.Transaction.Remarks : null,
                 Method = o.Transaction != null ? o.Transaction.Method : default,
                 Status = o.Status,
                 ShippingAddressId = o.ShippingAddressId,
                 RazorPageTransactionID = o.Transaction != null ? o.Transaction.RazorPageTransactionID : null,
                 Name = o.ShippingAddress != null ? o.ShippingAddress.Name : null,
                 Phone = o.ShippingAddress != null ? o.ShippingAddress.Phone : null,
                 Street = o.ShippingAddress != null ? o.ShippingAddress.Street : null,
                 City = o.ShippingAddress != null ? o.ShippingAddress.City : null,
                 State = o.ShippingAddress != null ? o.ShippingAddress.State : null,
                 PostalCode = o.ShippingAddress != null ? o.ShippingAddress.PostalCode : null,
                 Country = o.ShippingAddress != null ? o.ShippingAddress.Country : null
             });
            return Ok(pagedResult);

        }

        [HttpPut("cancel")]

        public IActionResult CancelOrderById([FromQuery] int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId ==null) return Unauthorized("User is Invalid");
            var order= _dbContext.Orders.FirstOrDefault(o => o.UserId == int.Parse(userId) && o.Id == id);

            if (order == null) return NotFound("Order not found.");

            order.Status =OrderStatus.Cancelled;
            _dbContext.SaveChanges();

            return Ok(new { 
            id = order.Id,
            message = "Cancelled Order "}
            );
        }
                         
          



    }
    
}
