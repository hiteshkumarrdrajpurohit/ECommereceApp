using EcommerceApp.DTO;
using EcommerceApp.Enum;
using EcommerceApp.Model;
using EcommerceApp.Shared.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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
            var item = _dbContext.Items.FirstOrDefault(i => i.Id == id && i.IsActive==true);
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

        [HttpGet("all")]
        public async Task<IActionResult> GetAllItems([FromQuery] PagingParameters paging)
        {
            var query = _dbContext
                        .Items
                        .Where(i => i.IsActive == true)
                        .AsNoTracking()
                        .OrderBy(i => i.Id);

            var pagedResult = await query
                           .ToPagedResponseAsync(
                            paging.Page,
                            paging.PageSize,
                            i => new ItemDTO
                            {
                             Name = i.Name,
                             Description = i.Description,
                             Price = i.Price,
                             Category = i.Category

                           });
            return Ok(pagedResult);
        }

        [HttpPut("update/{id}")]
        public IActionResult UpdateItem([FromBody] ItemDTO dto , [FromQuery] int id)
        {
            var item = _dbContext.Items.FirstOrDefault(i => i.Id == id && i.IsActive==true);

            if (item == null) return NotFound("Item not found");
            item.Name = dto.Name;
            item.Description = dto.Description;
            item.Price = dto.Price;
            item.Category = dto.Category;
            _dbContext.SaveChanges();
            return Ok("Item updated successfully");
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteItem([FromQuery] int id)
        {
            var item = _dbContext.Items.FirstOrDefault(i => i.Id == id && i.IsActive==true);
            if (item == null) return NotFound("Item not found");
            item.IsActive = false;
            item.Inventory!.IsActive = false;
            _dbContext.SaveChanges();

            return Ok(new { messagae = "Item deleted successfully" ,
                          id = item.Id});
        }
    }
}
