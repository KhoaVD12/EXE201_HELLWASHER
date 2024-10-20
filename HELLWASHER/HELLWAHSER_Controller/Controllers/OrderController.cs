using BusinessObject.IService;
using BusinessObject.ViewModels.OrderDTO;
using CloudinaryDotNet;
using DataAccess;
using DataAccess.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HELLWASHER_Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly Cloudinary _cloudinary;
        private readonly IOrderService _orderService;
        private readonly WashShopContext _context;
        public OrderController(IOrderService orderService, WashShopContext context)
        {
            _orderService = orderService;
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var result = await _orderService.GetAllOrder();
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            var result = await _orderService.GetOrderById(orderId);
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
        //[HttpPatch("add-confirm-image/{orderId}")]
        //public async Task<IActionResult> AddConfirmImage(int orderId,[FromForm] List<IFormFile> files)
        //{
        //    var order = await _context.Orders.FindAsync(orderId);
        //    if (order == null)
        //        return NotFound("Order not found");

        //    var uploadedImage = new List<string>();

        //    foreach (var file in files)
        //    {
        //        if (file.Length > 0)
        //        {
        //            using (var stream = file.OpenReadStream())
        //            {
        //                File = new FileDescription(file.FileName, stream);
        //            }
        //        }
        //    }
        //}
        [HttpPut("update/{orderId}")]
        public async Task<IActionResult> UpdateOrder([FromForm] UpdateOrderRequest orderRequest, int orderId)
        {
            var result = await _orderService.UpdateOrder(orderRequest, orderId);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

        [HttpPatch("update-status")]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, OrderStatusEnumRequest status)
        {
            var result = await _orderService.UpdateOrderStatus(orderId, status);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }
        [HttpPatch("image")]
        public async Task<IActionResult> AddImage(int orderId, string image)
        {
            var result = await _orderService.AddConfirmImage(orderId, image);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }
        [HttpPost("Confirm-email")]
        public async Task<IActionResult> SendConfirmOrderEmail(int orderId)
        {
            var result = await _orderService.SendConfirmOrderEmail(orderId);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }
    }
}
