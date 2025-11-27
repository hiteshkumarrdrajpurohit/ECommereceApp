using Microsoft.EntityFrameworkCore;

namespace EcommerceApp
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

  
    }
}