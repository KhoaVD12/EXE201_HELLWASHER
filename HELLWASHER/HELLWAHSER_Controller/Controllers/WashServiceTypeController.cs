using BusinessObject.IService;
using BusinessObject.Model.Request;
using BusinessObject.Model.Response;
using BusinessObject.Service;
using Microsoft.AspNetCore.Mvc;

namespace HELLWASHER_Controller.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WashServiceTypeController : ControllerBase
    {
        private readonly IWashServiceTypeService _washServiceTypeService;
        public WashServiceTypeController(IWashServiceTypeService washServiceTypeService)
        {
            _washServiceTypeService = washServiceTypeService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateWashServiceType(CreateWashServiceTypeDTO newWashServiceType)
        {
            var result = await _washServiceTypeService.CreateWashServiceType(newWashServiceType);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetWashServiceType(int page = 1, int pageSize = 10,
            string search = "", string sort = "")
        {
            var result = await _washServiceTypeService.GetAllWashServiceType(page, pageSize, search, sort);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWashServiceType(int id)
        {
            var result = await _washServiceTypeService.DeleteWashServiceType(id);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWashServiceType(int id, ResponseWashServiceTypeDTO newWashServiceType)
        {
            var result = await _washServiceTypeService.UpdateWashServiceType(id, newWashServiceType);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
