using Microsoft.EntityFrameworkCore;
using Simple_Ecommerce_Web_app.Models;

namespace Simple_Ecommerce_Web_app.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Inventory> Inventories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure User-Role relationship
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);

            // Configure Product-Category relationship
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure Product-Inventory relationship (one-to-one)
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Inventory)
                .WithOne(i => i.Product)
                .HasForeignKey<Inventory>(i => i.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed Roles
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = 1,
                    Name = "Admin",
                    Description = "Administrator with full access",
                    CanAddProducts = true,
                    CanEditPrices = true,
                    CanViewProducts = true,
                    CanAddToCart = true
                },
                new Role
                {
                    Id = 2,
                    Name = "Customer",
                    Description = "Regular customer",
                    CanAddProducts = false,
                    CanEditPrices = false,
                    CanViewProducts = true,
                    CanAddToCart = true
                }
            );

            // Seed Users (using simple password hashing for demo - in production use proper hashing)
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    Email = "admin@ecommerce.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                    RoleId = 1,
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new User
                {
                    Id = 2,
                    Username = "customer",
                    Email = "customer@ecommerce.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("customer123"),
                    RoleId = 2,
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                }
            );

            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Electronics",
                    Description = "Electronic devices and accessories"
                },
                new Category
                {
                    Id = 2,
                    Name = "Clothing",
                    Description = "Fashion and apparel"
                },
                new Category
                {
                    Id = 3,
                    Name = "Books",
                    Description = "Books and publications"
                },
                new Category
                {
                    Id = 4,
                    Name = "Home & Garden",
                    Description = "Home improvement and garden supplies"
                }
            );

            // Seed Products
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Laptop",
                    Description = "High-performance laptop for work and gaming",
                    Price = 999.99m,
                    ImageUrl = "https://via.placeholder.com/300x300?text=Laptop",
                    CategoryId = 1,
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Product
                {
                    Id = 2,
                    Name = "T-Shirt",
                    Description = "Comfortable cotton t-shirt",
                    Price = 19.99m,
                    ImageUrl = "https://via.placeholder.com/300x300?text=T-Shirt",
                    CategoryId = 2,
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Product
                {
                    Id = 3,
                    Name = "Programming Book",
                    Description = "Learn C# programming from scratch",
                    Price = 39.99m,
                    ImageUrl = "https://via.placeholder.com/300x300?text=Book",
                    CategoryId = 3,
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                }
            );

            // Seed Inventory
            modelBuilder.Entity<Inventory>().HasData(
                new Inventory
                {
                    Id = 1,
                    ProductId = 1,
                    StockQuantity = 50,
                    ReorderLevel = 10,
                    LastRestocked = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Inventory
                {
                    Id = 2,
                    ProductId = 2,
                    StockQuantity = 100,
                    ReorderLevel = 20,
                    LastRestocked = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Inventory
                {
                    Id = 3,
                    ProductId = 3,
                    StockQuantity = 5,
                    ReorderLevel = 10,
                    LastRestocked = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                }
            );
        }
    }
}
