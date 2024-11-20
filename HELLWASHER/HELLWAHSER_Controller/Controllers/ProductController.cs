using BusinessObject.IService;
using BusinessObject.Model.Request.CreateRequest;
using BusinessObject.Model.Request.UpdateRequest.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HELLWASHER_Controller.Controllers
{
    [EnableCors("Allow")]
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
        [AllowAnonymous]
        public async Task<IActionResult> GetAllProduct()
        {
            var result = await _productService.GetAllProduct();
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }
        [HttpGet("{productId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductById(int productId)
        {
            var result = await _productService.GetProductById(productId);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }
        [HttpPost("Add")]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> AddProduct([FromForm] CreateProductDTO productDTO)
        {
            var result = await _productService.CreateProduct(productDTO);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }
        [HttpPut("Update/Image")]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> AddProductImage(IFormFile image, int productId)
        {
            var result = await _productService.ProductImage(image, productId);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }
        [HttpPut("update/{productId}")]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> UpdateProduct([FromForm] UpdateProductDTO productDTO, int productId)
        {
            var result = await _productService.UpdateProduct(productDTO, productId);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("Delete/{productId}")]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var result = await _productService.DeleteProduct(productId);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }
    }
}
