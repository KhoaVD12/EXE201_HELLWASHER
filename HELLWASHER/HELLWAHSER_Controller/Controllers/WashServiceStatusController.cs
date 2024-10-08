using BusinessObject.IService;
using BusinessObject.Model.Request.CreateRequest;
using BusinessObject.Model.Response;
using Microsoft.AspNetCore.Mvc;

namespace HELLWASHER_Controller.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WashServiceStatusController : ControllerBase
    {
        private readonly IWashServiceStatusService _washServiceStatusService;
        public WashServiceStatusController(IWashServiceStatusService washServiceStatusService)
        {
            _washServiceStatusService = washServiceStatusService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateWashServiceStatus(CreateWashServiceStatusDTO newWashServiceStatus)
        {
            var result = await _washServiceStatusService.CreateWashServiceStatus(newWashServiceStatus);
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
        public async Task<IActionResult> GetWashServiceStatus(int page = 1, int pageSize = 10,
            string search = "", string sort = "")
        {
            var result = await _washServiceStatusService.GetAllWashServiceStatus(page, pageSize, search, sort);
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
        public async Task<IActionResult> DeleteWashServiceStatus(int id)
        {
            var result = await _washServiceStatusService.DeleteWashServiceStatus(id);
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
        public async Task<IActionResult> UpdateWashServiceStatus(int id, ResponseWashServiceStatusDTO newWashServiceStatus)
        {
            var result = await _washServiceStatusService.UpdateWashServiceStatus(id, newWashServiceStatus);
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
