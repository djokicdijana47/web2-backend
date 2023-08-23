namespace OnlineShopWebAPI.Model
{
    public enum OrderStatus
    {
        New,
        InProgress,
        Canceled,
        Completed
    }

    public class Order
    {
            public string Id { get; set; } = "";
            public string CustomerEmail { get; set; } = "";
            public string CustomerAddress { get; set; } = "";
            public List<Product> OrderedProducts { get; set; } = new List<Product>();
            public DateTime OrderPlacedTime { get; set; }
            public DateTime OrderCompletedTime { get; set; }
            public OrderStatus OrderStatus { get; set; }
            public double DeliveryCharge { get; set; }

            public Order()
            {
                Id = Guid.NewGuid().ToString();
                DeliveryCharge = 123;

            }
        }
}
