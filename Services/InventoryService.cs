using Simple_Ecommerce_Web_app.Data;
using Simple_Ecommerce_Web_app.Models;
using Microsoft.EntityFrameworkCore;

namespace Simple_Ecommerce_Web_app.Services
{
    public class InventoryService
    {
        private readonly AppDbContext _context;

        public InventoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ReduceStockAsync(int productId, int quantity)
        {
            var inventory = await _context.Inventories
                .FirstOrDefaultAsync(i => i.ProductId == productId);

            if (inventory == null)
                return false;

            if (inventory.StockQuantity < quantity)
                return false;

            inventory.StockQuantity -= quantity;
            inventory.LastSold = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CheckStockAvailabilityAsync(int productId, int quantity)
        {
            var inventory = await _context.Inventories
                .FirstOrDefaultAsync(i => i.ProductId == productId);

            if (inventory == null)
                return false;

            return inventory.StockQuantity >= quantity;
        }

        public async Task<int> GetStockQuantityAsync(int productId)
        {
            var inventory = await _context.Inventories
                .FirstOrDefaultAsync(i => i.ProductId == productId);

            return inventory?.StockQuantity ?? 0;
        }
    }
}
