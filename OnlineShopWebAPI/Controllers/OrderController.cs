using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShopWebAPI.DataTransferObject;
using OnlineShopWebAPI.Interface;

namespace OnlineShopWebAPI.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("getAllOrders")]
        [Authorize(Roles = "admin")]
        public IActionResult GetAllOrders()
        {
            _orderService.CheckCompletedOrders();
            return Ok(_orderService.GetAllOrders());
        }

        [HttpPost("cancelOrder")]
        public IActionResult CancelOrder(string orderId)
        {
            _orderService.CancelOrder(orderId);
            return Ok();
        }


        [HttpGet("getNonCanceledOrders")]
        [Authorize(Roles = "shopper")]
        public IActionResult GetNonCanceledOrders(string email)
        {
            _orderService.CheckCompletedOrders();
            return Ok(_orderService.GetShopperNonCanceledOrders(email));
        }

        [HttpGet("getCanceledOrders")]
        [Authorize(Roles = "shopper")]
        public IActionResult GetCanceledOrders(string email)
        {
            _orderService.CheckCompletedOrders();
            return Ok(_orderService.GetShopperCanceledOrders(email));
        }

        [HttpPost("createOrder")]
        [Authorize(Roles = "shopper")]
        public IActionResult CreateOrder(OrderDto orderDto)
        {
            try
            {
                bool res = _orderService.CreateOrder(orderDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("completeOrder")]
        [Authorize(Roles = "seller")]
        public IActionResult DeliverOrder(string orderId)
        {
            _orderService.CompleteOrder(orderId);
            return Ok();
        }


        [HttpGet("getNewOrdersSeller")]
        [Authorize(Roles = "seller")]
        public IActionResult GetNewOrdersMerchant(string email)
        {
            _orderService.CheckCompletedOrders();
            return Ok(_orderService.GetSellerNewOrders(email));
        }


        [HttpGet("getAllOrdersSeller")]
        [Authorize(Roles = "seller")]
        public IActionResult GetAllOrdersMerchant(string email)
        {
            _orderService.CheckCompletedOrders();
            return Ok(_orderService.GetSellerAllOrders(email));
        }
    }
}
