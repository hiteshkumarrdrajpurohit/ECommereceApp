using EcommerceApp.Database;
using EcommerceApp.DTO;
using EcommerceApp.Model;
using EcommerceApp.Shared.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles = "Admin,Seller")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public InventoryController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("add")]
        public IActionResult AddInventory([FromBody] InventoryDto inventoryDto)
        {
            var item = _dbContext.Inventories.FirstOrDefault(i => i.ItemId == inventoryDto.ItemId);

            if (item==null) return Ok("item does not exist ");

            var inventory = new Inventory
            {
                ItemId = inventoryDto.ItemId,
                QuantityAvailable = inventoryDto.QuantityAvailable,
                QuantityReserved = inventoryDto.QuantityReserved,
                QuantitySold = inventoryDto.QuantitySold,
                LowStockThreshold = inventoryDto.LowStockThreshold,
                Status = inventoryDto.Status
            };
            _dbContext.Inventories.Add(inventory);
            _dbContext.SaveChanges();
            return Ok("Inventory item added successfully");
        }

        [HttpPut("update")]
        public IActionResult UpdateInventory([FromBody] InventoryDto inventoryDto)
        {
            var inventory = _dbContext.Inventories.FirstOrDefault(i => i.ItemId == inventoryDto.ItemId);
            if (inventory == null) return NotFound("Inventory item not found");
            inventory.QuantityAvailable = inventoryDto.QuantityAvailable;
            inventory.QuantityReserved = inventoryDto.QuantityReserved;
            inventory.QuantitySold = inventoryDto.QuantitySold;
            inventory.LowStockThreshold = inventoryDto.LowStockThreshold;
            inventory.Status = inventoryDto.Status;
            _dbContext.SaveChanges();
            return Ok(new {
                itemId= inventoryDto.ItemId,
                message = "Inventory item updated successfully",
            }
            );
        }
        [HttpGet("{id}")]
        public IActionResult GetInvetoryByItemId(int id)
        {
                     var inventory = _dbContext.Inventories.FirstOrDefault( i => i.ItemId == id);
            if (inventory == null) return NotFound("Inventory item not found");

            return Ok(new InventoryDto{ ItemId = id, 
                           QuantityAvailable = inventory.QuantityAvailable,
                           QuantityReserved =inventory.QuantityReserved,
                           QuantitySold = inventory.QuantitySold,
                           LowStockThreshold = inventory.LowStockThreshold,
                            Status = inventory.Status
            });
        }

        [HttpGet("all")]

        public  async Task<IActionResult> GetAllInvetory([FromQuery] PagingParameters paging) {
          
           var query = _dbContext
                       .Inventories
                       .AsNoTracking()
                       .OrderBy(i => i.ItemId);
            var pagedResult = await query.ToPagedResponseAsync(
                paging.Page,
                paging.PageSize,
                i => new InventoryDto
                {
                    ItemId = i.ItemId,
                    LowStockThreshold = i.LowStockThreshold,
                    QuantityAvailable = i.QuantityAvailable,
                    QuantityReserved = i.QuantityReserved,
                    QuantitySold = i.QuantitySold,
                    Status = i.Status
                });
            return Ok(pagedResult);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteInventory(int id)
        {
            var inventory = _dbContext.Inventories.FirstOrDefault(i => i.ItemId == id);
            if (inventory == null) return NotFound("Inventory item not found");
            //_dbContext.Inventories.Remove(inventory);
            //_dbContext.SaveChanges();

            inventory.IsActive = false;
            _dbContext.SaveChanges();
            return Ok("Inventory item deleted successfully");
        }
    }
}
