using System.ComponentModel.DataAnnotations;

namespace Simple_Ecommerce_Web_app.Models
{
    public class Inventory
    {
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        [Required]
        public int StockQuantity { get; set; }

        [Required]
        public int ReorderLevel { get; set; }

        public DateTime LastRestocked { get; set; }
        public DateTime? LastSold { get; set; }
    }
}
