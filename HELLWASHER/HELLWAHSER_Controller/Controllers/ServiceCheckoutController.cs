using BusinessObject.IService;
using BusinessObject.Model.Request.CreateRequest;
using Microsoft.AspNetCore.Mvc;

namespace HELLWASHER_Controller.Controllers
{
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
        public async Task<IActionResult>CreateItem(CreateServiceCheckoutDTO itemDTO)
        {
            var result = await _service.CreateServiceCheckout(itemDTO);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpPut("UpdateClothWeight/{id}&&{weight}")]
        public async Task<IActionResult> UpdateClothWeight(int id, int weight)
        {
            var result = await _service.UpdateClothWeight(id, weight);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult>Delete(int id)
        {
            var result= await _service.DeleteCheckout(id);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpGet("OrderId/{id}")]
        public async Task<IActionResult> GetByOrderId(int id)
        {
            var result=await _service.GetCheckoutByOrderId(id);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result.Message);
            }
        }
    }
}
