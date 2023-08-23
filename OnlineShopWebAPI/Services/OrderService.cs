using AutoMapper;
using OnlineShopWebAPI.DAL;
using OnlineShopWebAPI.DataTransferObject;
using OnlineShopWebAPI.Interface;
using OnlineShopWebAPI.Model;

namespace OnlineShopWebAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepositoryWrapper repository;
        private readonly IMapper mapper;

        public OrderService(IRepositoryWrapper repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public void CancelOrder(string orderId)
        {
            Order canceledOrder = repository.Order.GetItemById(orderId);
            
            foreach(var product in canceledOrder.OrderedProducts)
            {
                repository.Product.GetItemById(product.Id).Quantity++;
            }

            canceledOrder.OrderStatus = OrderStatus.Canceled;
            repository.Save();
        }

        public void CheckCompletedOrders()
        {
            List<Order> orders = repository.Order.GetItems();

            foreach (Order order in orders)
            {
                if (DateTime.Now > order.OrderCompletedTime)
                {
                    order.OrderStatus = OrderStatus.Completed;
                }
            }
        }

        public void CompleteOrder(string orderId)
        {
            Order completedOrder = repository.Order.GetItemById(orderId);
            completedOrder.OrderCompletedTime = DateTime.Now;
            completedOrder.OrderStatus = OrderStatus.Completed;
            repository.Save();
        }

        public bool CreateOrder(OrderDto OrderDto)
        {
            var products = repository.Product.GetItems();

            var ord = mapper.Map<Order>(OrderDto);
            ord.Id = Guid.NewGuid().ToString();
            List<Product> productsList = new List<Product>(OrderDto.OrderedProducts.Count);
            foreach (var prod in OrderDto.OrderedProducts)
            {
                var old = products.FirstOrDefault(x => x.Id == prod.Id);
                old.Quantity -= 1;
                productsList.Add(old);
            }

            ord.OrderStatus = OrderStatus.InProgress;

            ord.OrderedProducts = productsList;

            var seed = 3;
            var random = new Random(seed);
            var rNum = random.Next(60, 360);

            ord.OrderPlacedTime = DateTime.Now;

            ord.OrderCompletedTime = DateTime.Now.AddMinutes((double)(rNum));

            repository.Order.AddItem(ord);
            repository.Save();
            return true;
        }

        public List<OrderDto> GetAllOrders()
        {
            return mapper.Map<List<OrderDto>>(repository.Order.GetItems().ToList());
        }

        public List<OrderDto> GetSellerAllOrders(string email)
        {
            List<Order> orders = new List<Order>(0);
            foreach (var order in repository.Order.GetItems())
            {
                if (order.OrderedProducts.Select(x => x.Seller == email).Any())
                {
                    orders.Add(order);
                }
            }
            return mapper.Map<List<OrderDto>>(orders);
        }

        public List<OrderDto> GetShopperCanceledOrders(string email)
        {
            var list = repository.Order.GetItems().Where(x => x.OrderStatus == OrderStatus.Canceled && x.CustomerEmail.Equals(email));
            return mapper.Map<List<OrderDto>>(repository.Order.GetItems().Where(x => x.OrderStatus == OrderStatus.Canceled && x.CustomerEmail.Equals(email)).ToList());
        }

        public List<OrderDto> GetSellerNewOrders(string email)
        {
            List<Order> orders = new List<Order>(0);
            var ords = repository.Order.GetItems();
            foreach (var order in repository.Order.GetItems().Where(x => x.OrderStatus == OrderStatus.New || x.OrderStatus == OrderStatus.InProgress))
            {
                if (order.OrderedProducts.Select(x => x.Seller == email).Any())
                {
                    orders.Add(order);
                }
            }
            return mapper.Map<List<OrderDto>>(orders);
        }

        public List<OrderDto> GetShopperNonCanceledOrders(string email)
        {
            return mapper.Map<List<OrderDto>>(repository.Order.GetItems().Where(x => x.OrderStatus != OrderStatus.Canceled && x.CustomerEmail.Equals(email)).ToList());

        }
    }
}
