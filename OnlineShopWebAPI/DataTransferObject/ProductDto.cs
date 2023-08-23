namespace OnlineShopWebAPI.DataTransferObject
{
    public class ProductDto
    {
        public string Id { get; set; } = "";
        public string ProductName { get; set; } = "";
        public string Description { get; set; } = "";
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string ProductImage { get; set; } = "";
        public string Seller { get; set; } = "";
    }
}
