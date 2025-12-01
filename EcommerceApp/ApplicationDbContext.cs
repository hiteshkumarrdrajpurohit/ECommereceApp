using EcommerceApp.Model;
using Microsoft.EntityFrameworkCore;
    
namespace EcommerceApp.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // USER ↔ ADDRESS (Many-to-Many)
            modelBuilder.Entity<User>()
                .HasMany(u => u.Addresses)
                .WithMany(a => a.Users)
                .UsingEntity(j => j.ToTable("UserAddresses"));

            // USER → ORDER (1:M)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.UserInfo)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);

            // ORDER → ORDER ITEM QUANTITY (1 : M)

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItemQuantity)
                .WithOne(oiq => oiq.Order)
                .HasForeignKey(oiq => oiq.OrderId);

            // ITEM → ORDER ITEM QUANTITY (1 : M)
            modelBuilder.Entity<OrderItemQuantity>()
              .HasOne(oiq => oiq.Item)
              .WithMany()
              .HasForeignKey(oiq => oiq.ItemId);
       
            // ITEM → INVENTORY (1:1)
            modelBuilder.Entity<Inventory>()
                .HasOne(inv => inv.Item)
                .WithOne(i => i.Inventory)
                .HasForeignKey<Inventory>(inv => inv.ItemId);

            // ORDER → TRANSACTION (1:M)
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Order)
                .WithMany(o => o.Transactions)
                .HasForeignKey(t => t.OrderId);

            // ORDER → SHIPPING ADDRESS (M:1)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.ShippingAddress)
                .WithMany()
                .HasForeignKey(o => o.ShippingAddressId);


            modelBuilder.Entity<Item>()
                .Property(i => i.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasPrecision(18, 2);
        }
    }
}