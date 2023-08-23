namespace OnlineShopWebAPI.Model
{
    public class Product
    {
        public string Id { get; set; }
        public string ProductName { get; set; } = "";
        public string Description { get; set; } = "";
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string ProductImage { get; set; } = "";

        public string Seller { get; set; } = "";

        public List<Order> Orders { get; set; } = new List<Order>();

        public Product()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
