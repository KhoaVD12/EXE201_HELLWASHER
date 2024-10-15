using BusinessObject.IService;
using BusinessObject.ViewModels.OrderDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HELLWASHER_Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var result = await _orderService.GetAllOrder();
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }
        [HttpPost("Add/{userId}")]
        public async Task<IActionResult> AddOrder([FromBody] OrderDTO orderDTO, int userId)
        {
            var result = await _orderService.AddOrder(orderDTO, userId);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("update/{orderId}")]
        public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrderRequest orderRequest, int orderId)
        {
            var result = await _orderService.UpdateOrder(orderRequest, orderId);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

        //[HttpPut("updateStatus/{orderId}")]
        //public async Task<IActionResult> UpdateOrderStatus(int orderId, string status)
        //{
        //    var result = await _orderService.UpdateOrderStatus(orderId, status);
        //    if (!result.Success) return BadRequest(result);

        //    return Ok(result);
        //}
    }
}
