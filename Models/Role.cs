namespace Simple_Ecommerce_Web_app.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool CanAddProducts { get; set; }
        public bool CanEditPrices { get; set; }
        public bool CanViewProducts { get; set; }
        public bool CanAddToCart { get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
