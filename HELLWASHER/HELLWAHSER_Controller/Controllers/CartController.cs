using BusinessObject.IService;
using BusinessObject.Model.Request.CreateRequest;
using Microsoft.AspNetCore.Mvc;

namespace HELLWASHER_Controller.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService service)
        {
            _cartService = service;
        }
        [HttpGet("Carts")]
        public async Task<IActionResult> GetAll()
        {
            var result= await _cartService.GetCarts();
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult>CreateCart(CreateCartDTO cartDTO)
        {
            var result = await _cartService.CreateCart(cartDTO);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpPut("ChangeCartStatus/{id}&{status}")]
        public async Task<IActionResult>ChangeCartStatus(int id, string status)
        {
            var result = await _cartService.ChangeCartStatus(id, status);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpPut("UpdateCartTotalPrice/{id}")]
        public async Task<IActionResult> UpdateCartTotalPrice(int id)
        {
            var result = await _cartService.UpdateCartTotalPrice(id);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
    }
}
