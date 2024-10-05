using BusinessObject.IService;
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
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result= await _cartService.GetCarts();
            return Ok(result);
        }
    }
}
