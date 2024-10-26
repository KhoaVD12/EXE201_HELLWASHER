using BusinessObject.IService;
using BusinessObject.ViewModels.Order;
using BusinessObject.ViewModels.OrderDTO;
using CloudinaryDotNet;
using DataAccess;
using DataAccess.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HELLWASHER_Controller.Controllers
{
    [EnableCors("Allow")]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        private readonly WashShopContext _context;
        public OrderController(IOrderService orderService, WashShopContext context, IUserService userService)
        {
            _orderService = orderService;
            _context = context;
            _userService = userService;
        }
        [HttpGet]
        [Authorize(Roles = "Admin, Staff, Customer")]
        public async Task<IActionResult> GetAllOrders()
        {
            var user = await _userService.GetUserByTokenAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var result = await _orderService.GetAllOrder(user);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("{orderId}")]
        [Authorize(Roles = "Admin, Staff, Customer")]
        public async Task<IActionResult> GetOrderById([FromRoute] int orderId)
        {
            var user = await _userService.GetUserByTokenAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }
            var result = await _orderService.GetOrderById(orderId, user);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }
        [HttpPost("QuickAdd")]
        [AllowAnonymous]
        public async Task<IActionResult> QuickAddOrder([FromBody] QuickOrderDTO orderDTO)
        {
            var result = await _orderService.QuickAddOrder(orderDTO);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("Add")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> AddOrder([FromBody] OrderDTO orderDTO)
        {
            var user = await _userService.GetUserByTokenAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var result = await _orderService.AddOrder(orderDTO, user);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("update/{orderId}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrderRequest orderRequest,[FromRoute] int orderId)
        {
            var user = await _userService.GetUserByTokenAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var result = await _orderService.UpdateOrder(orderRequest, orderId, user);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

        [HttpPatch("update-status")]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> UpdateOrderStatus([FromRoute] int orderId, OrderStatusEnumRequest status)
        {
            var result = await _orderService.UpdateOrderStatus(orderId, status);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }
        [HttpPatch("image")]
        [Authorize(Roles = "Staff, Customer")]
        public async Task<IActionResult> AddImage([FromRoute] int orderId, IFormFile image)
        {
            var result = await _orderService.AddConfirmImage(orderId, image);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }
        [HttpPost("Confirm-email")]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> SendConfirmOrderEmail([FromRoute] int orderId)
        {
            var result = await _orderService.SendConfirmOrderEmail(orderId);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }
    }
}
