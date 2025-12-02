using EcommerceApp.Enum;
using System.ComponentModel.DataAnnotations;

namespace EcommerceApp.DTO
{
    public class ItemDTO
    {
        [Required]
        public string Name { get; set; }= string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public decimal Price { get; set; }

        [Required]
        public ItemCategory Category { get; set; }
    }
}
