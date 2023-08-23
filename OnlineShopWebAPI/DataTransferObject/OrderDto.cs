using OnlineShopWebAPI.Model;

namespace OnlineShopWebAPI.DataTransferObject
{
    public class OrderDto
    {
        public string Id { get; set; }
        public string CustomerEmail { get; set; } = "";
        public string CustomerAddress { get; set; } = "";
        public DateTime OrderPlacedTime { get; set; }
        public DateTime OrderCompletedTime { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public double DeliveryCharge { get; set; }
        public List<ProductDto> OrderedProducts { get; set; } = new List<ProductDto>();
    }
}
