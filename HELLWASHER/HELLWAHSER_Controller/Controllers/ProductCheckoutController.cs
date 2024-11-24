using BusinessObject.IService;
using BusinessObject.ViewModels.ProductCheckoutDTO;
using DataAccess.Entity;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Customer, Staff")]
        public async Task<IActionResult> CreateProductCheckout(int orderId, [FromBody]ProductCheckoutDTO productCheckoutDTO)
        {
            var result = await _productCheckoutService.CreateProductCheckout(orderId, productCheckoutDTO);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }
        [HttpPut("update/{id}&{quantity}")]
        [Authorize(Roles = "Customer, Staff")]
        public async Task<IActionResult> UpdateProductCheckout([FromRoute]int id, [FromRoute]int quantity)
        {
            var result = await _productCheckoutService.UpdateProductCheckout(id, quantity);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }
        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Customer, Staff")]
        public async Task<IActionResult> DeleteProductCheckout(int id)
        {
            var result = await _productCheckoutService.DeleteProductCheckout(id);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }
        [HttpGet("OrderId/{id}")]
        [Authorize(Roles = "Customer, Staff")]
        public async Task<IActionResult> GetByOrderId([FromRoute] int id)
        {
            var result = await _productCheckoutService.GetCheckoutByOrderId(id);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }
    }
}
