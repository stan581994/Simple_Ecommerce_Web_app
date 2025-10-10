using Microsoft.EntityFrameworkCore;
using Simple_Ecommerce_Web_app.Data;
using Simple_Ecommerce_Web_app.Models;

namespace Simple_Ecommerce_Web_app.Services
{
    public class ProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Inventory)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<Product>> GetLowStockProductsAsync()
        {
            return await _context.Products
                .Include(p => p.Inventory)
                .Include(p => p.Category)
                .Where(p => p.Inventory != null && p.Inventory.StockQuantity <= p.Inventory.ReorderLevel)
                .ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Inventory)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> CreateProductAsync(Product product, int initialStock, int reorderLevel)
        {
            product.CreatedAt = DateTime.UtcNow;
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            var inventory = new Inventory
            {
                ProductId = product.Id,
                StockQuantity = initialStock,
                ReorderLevel = reorderLevel,
                LastRestocked = DateTime.UtcNow
            };

            _context.Inventories.Add(inventory);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            product.UpdatedAt = DateTime.UtcNow;
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _context.Products
                .Include(p => p.Inventory)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return false;

            if (product.Inventory != null)
            {
                _context.Inventories.Remove(product.Inventory);
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateStockAsync(int productId, int quantityChange)
        {
            var inventory = await _context.Inventories
                .FirstOrDefaultAsync(i => i.ProductId == productId);

            if (inventory == null)
                return false;

            inventory.StockQuantity += quantityChange;

            if (quantityChange > 0)
            {
                inventory.LastRestocked = DateTime.UtcNow;
            }
            else if (quantityChange < 0)
            {
                inventory.LastSold = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }
    }
}
