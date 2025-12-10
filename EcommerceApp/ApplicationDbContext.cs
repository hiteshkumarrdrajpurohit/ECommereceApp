using EcommerceApp.Model;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp
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
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        
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
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);


            // ORDER → TRANSACTION (1:1)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Transaction)
                .WithOne(t => t.Order)
                .HasForeignKey<Transaction>(t => t.OrderId);


            // ITEM → ORDER ITEM QUANTITY (1:M)
            modelBuilder.Entity<OrderItemQuantity>()
                .HasOne(oiq => oiq.Item)
                .WithMany()
                .HasForeignKey(oiq => oiq.ItemId);

            // ITEM → INVENTORY (1:1)
            modelBuilder.Entity<Item>()
                .HasOne(i => i.Inventory)
                .WithOne(inv => inv.Item)
                .HasForeignKey<Inventory>(inv => inv.ItemId);


            // ORDER → SHIPPING ADDRESS (M:1)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.ShippingAddress)
                .WithMany()
                .HasForeignKey(o => o.ShippingAddressId);

            // PRICE PRECISION
            modelBuilder.Entity<Item>()
                .Property(i => i.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasPrecision(18, 2);


            modelBuilder.Entity<Cart>()
                .Property(o => o.TotalAmount)
                .HasPrecision(18, 2);

            // USER ↔ CART (1:1)
            modelBuilder.Entity<User>()
                .HasOne(u => u.Cart)
                .WithOne(c => c.User)
                .HasForeignKey<Cart>(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Cart>()
                        .HasKey(c => c.Id);
            // CART → CARTITEM (1:M)
            modelBuilder.Entity<Cart>()
                .HasMany(c => c.Items)
                .WithOne(ci => ci.Cart)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.Cascade);

            // CARTITEM → ITEM (1:1)
            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Item)
                .WithMany()
                .HasForeignKey(ci => ci.ItemId);

            
        }
    }
}
