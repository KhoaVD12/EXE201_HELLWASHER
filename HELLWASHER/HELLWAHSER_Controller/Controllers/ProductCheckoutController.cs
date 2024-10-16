using BusinessObject.IService;
using BusinessObject.ViewModels.ProductCheckoutDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HELLWASHER_Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCheckoutController : ControllerBase
    {
        private readonly IProductCheckoutService _productCheckoutService;
        public ProductCheckoutController(IProductCheckoutService productCheckoutService)
        {
            _productCheckoutService = productCheckoutService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateProductCheckout(ProductCheckoutDTO productCheckoutDTO)
        {
            var result = await _productCheckoutService.CreateProductCheckout(productCheckoutDTO);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }
        [HttpPut("update/{id}&{quantity}")]
        public async Task<IActionResult> UpdateProductCheckout(int id, int quantity)
        {
            var result = await _productCheckoutService.UpdateProductCheckout(id, quantity);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteProductCheckout(int id)
        {
            var result = await _productCheckoutService.DeleteProductCheckout(id);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }
    }
}
