using EcommerceApp.Database;
using EcommerceApp.DTO;
using EcommerceApp.Enum;
using EcommerceApp.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EcommerceApp.Controllers
{
    [Route("[controller]")]
  
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public ItemController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("add")]
        public IActionResult AddItem([FromBody] ItemDTO dto)
        {
            Item newItem = new Item
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Category = dto.Category
            };

            _dbContext.Items.Add(newItem);

            var inventory = new Inventory
            {
                Item = newItem,
            };

            _dbContext.Inventories.Add(inventory);

            _dbContext.SaveChanges();
            
            return Ok(new { Message = "Item added successfully", ItemId = newItem.Id });

        }
        [HttpGet("getItemById")]
        public IActionResult GetItemById([FromQuery] int id)
        {
            var item = _dbContext.Items.FirstOrDefault(i => i.Id == id);
            if (item==null) return NotFound("Item not found");

            var responseItem = new ItemDTO
            {
                Name=item.Name,
                Description = item.Description,
               Price= item.Price,
               Category = item.Category 
            };

            return Ok(responseItem);
        }

    }
}
