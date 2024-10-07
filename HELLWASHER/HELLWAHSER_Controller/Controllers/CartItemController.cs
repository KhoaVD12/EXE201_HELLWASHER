using BusinessObject.IService;
using BusinessObject.Model.Request.CreateRequest;
using Microsoft.AspNetCore.Mvc;

namespace HELLWASHER_Controller.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartItemController : Controller
    {
        private readonly ICartItemService _service;
        public CartItemController(ICartItemService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult>CreateItem(CreateCartItemDTO itemDTO)
        {
            var result = await _service.CreateCartItem(itemDTO);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpPut("ChangeItemQuantity/{id}&&{quantity}")]
        public async Task<IActionResult>UpdateCartItemQuantity(int id, int quantity)
        {
            var result = await _service.UpdateCartItemQuantity(id, quantity);
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
