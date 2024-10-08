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
        [HttpPost("Add")]
        public async Task<IActionResult> AddOrder([FromBody] OrderDTO orderDTO)
        {
            var result = await _orderService.AddOrder(orderDTO);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrderRequest orderRequest)
        {
            var result = await _orderService.UpdateOrder(orderRequest);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }
    }
}
