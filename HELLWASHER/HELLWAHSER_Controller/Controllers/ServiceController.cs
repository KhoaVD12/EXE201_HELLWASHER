using BusinessObject.IService;
using BusinessObject.Model.Request.CreateRequest;
using BusinessObject.Model.Request.UpdateRequest.Entity;
using BusinessObject.Model.Response;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Mvc;

namespace HELLWASHER_Controller.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceController : Controller
    {
        private readonly IWashServiceService _washServiceService;
        public ServiceController(IWashServiceService service)
        {
            _washServiceService = service;
        }
        [HttpPost]
        public async Task<IActionResult> CreateService([FromForm] CreateWashServiceDTO serviceDTO)
        {
            var result = await _washServiceService.CreateWashService(serviceDTO);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetServices(int page = 1, int pageSize = 10,
            string search = "", string sort = "")
        {
            var result = await _washServiceService.GetAllWashService(page, pageSize, search, sort);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult>GetById(int id)
        {
            var result = await _washServiceService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateService(int id, UpdateWashServiceDTO serviceDTO)
        {
            var result = await _washServiceService.UpdateWashService(id, serviceDTO);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpPut("UpdateStatus/{id}&&{status}")]
        public async Task<IActionResult> ChangeStatusService(int id, string status)
        {
            var result = await _washServiceService.UpdateWashStatus(id, status);
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
        public async Task<IActionResult>DeleteService(int id)
        {
            var result = await _washServiceService.DeleteWashService(id);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
    }
}
