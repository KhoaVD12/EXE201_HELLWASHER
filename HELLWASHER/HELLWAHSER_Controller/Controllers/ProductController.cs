using BusinessObject.IService;
using BusinessObject.Model.Request.CreateRequest;
using BusinessObject.Model.Request.UpdateRequest.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HELLWASHER_Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
            var result = await _productService.GetAllProduct();
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }
        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductById(int productId)
        {
            var result = await _productService.GetProductById(productId);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }
        [HttpPost("Add")]
        public async Task<IActionResult> AddProduct([FromBody] CreateProductDTO productDTO)
        {
            var result = await _productService.CreateProduct(productDTO);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("update/{productId}")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductDTO productDTO, int productId)
        {
            var result = await _productService.UpdateProduct(productDTO, productId);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var result = await _productService.DeleteProduct(productId);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }
    }
}
