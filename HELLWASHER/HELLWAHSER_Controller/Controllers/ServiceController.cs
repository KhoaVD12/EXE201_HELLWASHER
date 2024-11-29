using BusinessObject.IService;
using BusinessObject.Model.Request.CreateRequest;
using BusinessObject.Model.Request.UpdateRequest.Entity;
using BusinessObject.Model.Response;
using CloudinaryDotNet;
using DataAccess.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace HELLWASHER_Controller.Controllers
{
    [EnableCors("Allow")]
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
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> CreateService([FromForm] CreateWashServiceDTO serviceDTO)
        {
            var result = await _washServiceService.CreateWashService(serviceDTO);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpGet]
        [AllowAnonymous]
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
                return NotFound(result);
            }
        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult>GetById([FromRoute]int id)
        {
            var result = await _washServiceService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> UpdateService([FromRoute] int id, [FromForm] UpdateWashServiceDTO serviceDTO)
        {
            var result = await _washServiceService.UpdateWashService(id, serviceDTO);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpPut("UpdateStatus/{id}&&{status}")]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> ChangeStatusService([FromRoute] int id, ServiceEnum status)
        {
            var result = await _washServiceService.UpdateWashStatus(id, status);
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
        public async Task<IActionResult>DeleteService([FromRoute] int id)
        {
            var result = await _washServiceService.DeleteWashService(id);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
