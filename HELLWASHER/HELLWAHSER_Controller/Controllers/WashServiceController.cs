using BusinessObject.IService;
using BusinessObject.Model.Request;
using BusinessObject.Model.Response;
using Microsoft.AspNetCore.Mvc;

namespace HELLWASHER_Controller.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WashServiceController : Controller
    {
        private readonly IWashServiceService _washServiceService;
        public WashServiceController(IWashServiceService service)
        {
            _washServiceService = service;
        }
        [HttpPost]
        public async Task<IActionResult> CreateService(CreateWashServiceDTO serviceDTO)
        {
            var result = await _washServiceService.CreateWashService(serviceDTO);
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
        public async Task<IActionResult> GetServices(int page = 1, int pageSize = 10,
            string search = "", string sort = "")
        {
            var result = await _washServiceService.GetAllWashService(page, pageSize, search, sort);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateService(int id, ResponseWashServiceDTO serviceDTO)
        {
            var result = await _washServiceService.UpdateWashService(id, serviceDTO);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult>DeleteService(int id)
        {
            var result = await _washServiceService.DeleteWashService(id);
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
