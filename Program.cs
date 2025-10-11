using Simple_Ecommerce_Web_app.Components;
using Simple_Ecommerce_Web_app.Data;
using Simple_Ecommerce_Web_app.Services;
using Simple_Ecommerce_Web_app.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add DbContext with SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=ecommerce.db"));

// Add Authentication Service as Singleton
builder.Services.AddSingleton<AuthService>();

// Add Product Service as Scoped
builder.Services.AddScoped<ProductService>();

// Add Category Service as Scoped
builder.Services.AddScoped<CategoryService>();

// Add Cart Service as Singleton
builder.Services.AddSingleton<CartService>();

var app = builder.Build();

// Ensure database is created and migrations are applied
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
    
    // Seed database with initial data
    SeedDatabase(dbContext);
}

void SeedDatabase(AppDbContext context)
{
    // Check if categories already exist
    if (!context.Categories.Any())
    {
        var categories = new[]
        {
            new Category { Name = "Women's Fashion", Description = "Clothing and accessories for women" },
            new Category { Name = "Men's Fashion", Description = "Clothing and accessories for men" },
            new Category { Name = "Accessories", Description = "Fashion accessories" },
            new Category { Name = "Shoes", Description = "Footwear for all occasions" }
        };
        context.Categories.AddRange(categories);
        context.SaveChanges();
    }

    // Check if products already exist
    if (!context.Products.Any())
    {
        var womenCategory = context.Categories.First(c => c.Name == "Women's Fashion");
        var menCategory = context.Categories.First(c => c.Name == "Men's Fashion");
        var accessoriesCategory = context.Categories.First(c => c.Name == "Accessories");
        var shoesCategory = context.Categories.First(c => c.Name == "Shoes");

        var products = new[]
        {
            new Product { Name = "Elegant Summer Dress", Description = "Perfect for any occasion with its flowing design and comfortable fabric.", Price = 89.99m, ImageUrl = "https://images.unsplash.com/photo-1515372039744-b8f02a3ae446?ixlib=rb-4.0.3&auto=format&fit=crop&w=500&q=80", CategoryId = womenCategory.Id },
            new Product { Name = "Classic Denim Jacket", Description = "Timeless style meets modern comfort in this versatile denim jacket.", Price = 79.99m, ImageUrl = "https://images.unsplash.com/photo-1551028719-00167b16eac5?ixlib=rb-4.0.3&auto=format&fit=crop&w=500&q=80", CategoryId = womenCategory.Id },
            new Product { Name = "Premium Leather Handbag", Description = "Crafted from genuine leather with attention to every detail.", Price = 199.99m, ImageUrl = "https://images.unsplash.com/photo-1584917865442-de89df76afd3?ixlib=rb-4.0.3&auto=format&fit=crop&w=500&q=80", CategoryId = accessoriesCategory.Id },
            new Product { Name = "Casual Cotton T-Shirt", Description = "Soft, breathable cotton in a relaxed fit for everyday comfort.", Price = 24.99m, ImageUrl = "https://images.unsplash.com/photo-1521572163474-6864f9cf17ab?ixlib=rb-4.0.3&auto=format&fit=crop&w=500&q=80", CategoryId = menCategory.Id },
            new Product { Name = "Designer Sunglasses", Description = "UV protection meets style in these trendy designer frames.", Price = 149.99m, ImageUrl = "https://images.unsplash.com/photo-1572635196237-14b3f281503f?ixlib=rb-4.0.3&auto=format&fit=crop&w=500&q=80", CategoryId = accessoriesCategory.Id },
            new Product { Name = "Cozy Knit Sweater", Description = "Stay warm and stylish with this soft, comfortable knit sweater.", Price = 69.99m, ImageUrl = "https://images.unsplash.com/photo-1434389677669-e08b4cac3105?ixlib=rb-4.0.3&auto=format&fit=crop&w=500&q=80", CategoryId = womenCategory.Id },
            new Product { Name = "Athletic Sneakers", Description = "Performance meets style in these comfortable athletic shoes.", Price = 119.99m, ImageUrl = "https://images.unsplash.com/photo-1549298916-b41d501d3772?ixlib=rb-4.0.3&auto=format&fit=crop&w=500&q=80", CategoryId = shoesCategory.Id },
            new Product { Name = "Silk Scarf", Description = "Add elegance to any outfit with this luxurious silk scarf.", Price = 59.99m, ImageUrl = "https://images.unsplash.com/photo-1601924994987-69e26d50dc26?ixlib=rb-4.0.3&auto=format&fit=crop&w=500&q=80", CategoryId = accessoriesCategory.Id },
            new Product { Name = "Business Suit", Description = "Professional attire for the modern businessman.", Price = 299.99m, ImageUrl = "https://images.unsplash.com/photo-1594938298603-c8148c4dae35?ixlib=rb-4.0.3&auto=format&fit=crop&w=500&q=80", CategoryId = menCategory.Id },
            new Product { Name = "Leather Boots", Description = "Durable and stylish boots for any weather.", Price = 179.99m, ImageUrl = "https://images.unsplash.com/photo-1544966503-7cc5ac882d5f?ixlib=rb-4.0.3&auto=format&fit=crop&w=500&q=80", CategoryId = shoesCategory.Id },
            new Product { Name = "Floral Print Blouse", Description = "Feminine and elegant blouse with beautiful floral patterns.", Price = 54.99m, ImageUrl = "https://images.unsplash.com/photo-1564257577-2d3b9b2c1e8f?ixlib=rb-4.0.3&auto=format&fit=crop&w=500&q=80", CategoryId = womenCategory.Id },
            new Product { Name = "Vintage Watch", Description = "Classic timepiece with modern functionality.", Price = 249.99m, ImageUrl = "https://images.unsplash.com/photo-1524592094714-0f0654e20314?ixlib=rb-4.0.3&auto=format&fit=crop&w=500&q=80", CategoryId = accessoriesCategory.Id }
        };
        context.Products.AddRange(products);
        context.SaveChanges();

        // Add inventory for each product
        foreach (var product in products)
        {
            context.Inventories.Add(new Inventory
            {
                ProductId = product.Id,
                StockQuantity = 50,
                ReorderLevel = 10
            });
        }
        context.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
