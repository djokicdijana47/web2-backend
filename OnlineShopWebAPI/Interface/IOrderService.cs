using OnlineShopWebAPI.DataTransferObject;

namespace OnlineShopWebAPI.Interface
{
    public interface IOrderService
    {
        List<OrderDto> GetShopperCanceledOrders(string email);
        List<OrderDto> GetAllOrders();
        bool CreateOrder(OrderDto orderDto);
        void CompleteOrder(string orderId);
        void CancelOrder(string orderId);
        List<OrderDto> GetShopperNonCanceledOrders(string email);
        List<OrderDto> GetSellerNewOrders(string email);
        List<OrderDto> GetSellerAllOrders(string email);
        void CheckCompletedOrders();
    }
}
