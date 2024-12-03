using BusinessObject.IService;
using BusinessObject.Model.Request.CreateRequest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace HELLWASHER_Controller.Controllers
{
    [EnableCors("AllowAll")]
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceCheckoutController : ControllerBase
    {
        private readonly IServiceCheckoutService _service;
        public ServiceCheckoutController(IServiceCheckoutService service)
        {
            _service = service;
        }
        [HttpPost]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult>CreateItem(int orderId, [FromBody] CreateServiceCheckoutDTO itemDTO)
        {
            var result = await _service.CreateServiceCheckout(orderId, itemDTO);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpPut("UpdateClothWeight/{id}&&{weight}")]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> UpdateClothWeight([FromRoute]int id, [FromRoute]decimal weight)
        {
            var result = await _service.UpdateClothWeight(id, weight);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult>Delete([FromRoute] int id)
        {
            var result= await _service.DeleteCheckout(id);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpGet("OrderId/{id}")]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> GetByOrderId([FromRoute] int id)
        {
            var result=await _service.GetCheckoutByOrderId(id);
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
