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
                new Category { Id = 1, Name = "Women's Fashion", Description = "Clothing and accessories for women" },
                new Category { Id = 2, Name = "Men's Fashion", Description = "Clothing and accessories for men" },
                new Category { Id = 3, Name = "Accessories", Description = "Fashion accessories" },
                new Category { Id = 4, Name = "Shoes", Description = "Footwear for all occasions" }
            );

            // Seed Products
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Elegant Summer Dress", Description = "Perfect for any occasion with its flowing design and comfortable fabric.", Price = 89.99m, OriginalPrice = 129.99m, IsOnSale = true, ImageUrl = "https://images.unsplash.com/photo-1515372039744-b8f02a3ae446?ixlib=rb-4.0.3&auto=format&fit=crop&w=500&q=80", CategoryId = 1, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Product { Id = 2, Name = "Classic Denim Jacket", Description = "Timeless style meets modern comfort in this versatile denim jacket.", Price = 79.99m, IsNew = true, ImageUrl = "https://images.unsplash.com/photo-1551028719-00167b16eac5?ixlib=rb-4.0.3&auto=format&fit=crop&w=500&q=80", CategoryId = 1, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Product { Id = 3, Name = "Premium Leather Handbag", Description = "Crafted from genuine leather with attention to every detail.", Price = 199.99m, ImageUrl = "https://images.unsplash.com/photo-1584917865442-de89df76afd3?ixlib=rb-4.0.3&auto=format&fit=crop&w=500&q=80", CategoryId = 3, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Product { Id = 4, Name = "Casual Cotton T-Shirt", Description = "Soft, breathable cotton in a relaxed fit for everyday comfort.", Price = 24.99m, OriginalPrice = 34.99m, IsOnSale = true, ImageUrl = "https://images.unsplash.com/photo-1521572163474-6864f9cf17ab?ixlib=rb-4.0.3&auto=format&fit=crop&w=500&q=80", CategoryId = 2, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Product { Id = 5, Name = "Designer Sunglasses", Description = "UV protection meets style in these trendy designer frames.", Price = 149.99m, IsNew = true, ImageUrl = "https://images.unsplash.com/photo-1572635196237-14b3f281503f?ixlib=rb-4.0.3&auto=format&fit=crop&w=500&q=80", CategoryId = 3, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Product { Id = 6, Name = "Cozy Knit Sweater", Description = "Stay warm and stylish with this soft, comfortable knit sweater.", Price = 69.99m, ImageUrl = "https://images.unsplash.com/photo-1434389677669-e08b4cac3105?ixlib=rb-4.0.3&auto=format&fit=crop&w=500&q=80", CategoryId = 1, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Product { Id = 7, Name = "Athletic Sneakers", Description = "Performance meets style in these comfortable athletic shoes.", Price = 119.99m, OriginalPrice = 159.99m, IsOnSale = true, ImageUrl = "https://images.unsplash.com/photo-1549298916-b41d501d3772?ixlib=rb-4.0.3&auto=format&fit=crop&w=500&q=80", CategoryId = 4, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Product { Id = 8, Name = "Silk Scarf", Description = "Add elegance to any outfit with this luxurious silk scarf.", Price = 59.99m, IsNew = true, ImageUrl = "https://images.unsplash.com/photo-1601924994987-69e26d50dc26?ixlib=rb-4.0.3&auto=format&fit=crop&w=500&q=80", CategoryId = 3, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Product { Id = 9, Name = "Business Suit", Description = "Professional attire for the modern businessman.", Price = 299.99m, ImageUrl = "https://images.unsplash.com/photo-1594938298603-c8148c4dae35?ixlib=rb-4.0.3&auto=format&fit=crop&w=500&q=80", CategoryId = 2, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Product { Id = 10, Name = "Leather Boots", Description = "Durable and stylish boots for any weather.", Price = 179.99m, OriginalPrice = 219.99m, IsOnSale = true, ImageUrl = "https://images.unsplash.com/photo-1544966503-7cc5ac882d5f?ixlib=rb-4.0.3&auto=format&fit=crop&w=500&q=80", CategoryId = 4, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Product { Id = 11, Name = "Floral Print Blouse", Description = "Feminine and elegant blouse with beautiful floral patterns.", Price = 54.99m, IsNew = true, ImageUrl = "https://images.unsplash.com/photo-1564257577-2d3b9b2c1e8f?ixlib=rb-4.0.3&auto=format&fit=crop&w=500&q=80", CategoryId = 1, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Product { Id = 12, Name = "Vintage Watch", Description = "Classic timepiece with modern functionality.", Price = 249.99m, ImageUrl = "https://images.unsplash.com/photo-1524592094714-0f0654e20314?ixlib=rb-4.0.3&auto=format&fit=crop&w=500&q=80", CategoryId = 3, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
            );

            // Seed Inventory
            modelBuilder.Entity<Inventory>().HasData(
                new Inventory { Id = 1, ProductId = 1, StockQuantity = 50, ReorderLevel = 10, LastRestocked = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Inventory { Id = 2, ProductId = 2, StockQuantity = 50, ReorderLevel = 10, LastRestocked = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Inventory { Id = 3, ProductId = 3, StockQuantity = 50, ReorderLevel = 10, LastRestocked = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Inventory { Id = 4, ProductId = 4, StockQuantity = 50, ReorderLevel = 10, LastRestocked = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Inventory { Id = 5, ProductId = 5, StockQuantity = 50, ReorderLevel = 10, LastRestocked = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Inventory { Id = 6, ProductId = 6, StockQuantity = 50, ReorderLevel = 10, LastRestocked = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Inventory { Id = 7, ProductId = 7, StockQuantity = 50, ReorderLevel = 10, LastRestocked = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Inventory { Id = 8, ProductId = 8, StockQuantity = 50, ReorderLevel = 10, LastRestocked = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Inventory { Id = 9, ProductId = 9, StockQuantity = 50, ReorderLevel = 10, LastRestocked = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Inventory { Id = 10, ProductId = 10, StockQuantity = 50, ReorderLevel = 10, LastRestocked = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Inventory { Id = 11, ProductId = 11, StockQuantity = 50, ReorderLevel = 10, LastRestocked = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Inventory { Id = 12, ProductId = 12, StockQuantity = 50, ReorderLevel = 10, LastRestocked = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
            );
        }
    }
}
