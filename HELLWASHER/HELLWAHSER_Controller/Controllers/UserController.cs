using BusinessObject.IService;
using BusinessObject.Model.Request.CreateRequest;
using BusinessObject.Model.Request.UpdateRequest.Entity;
using BusinessObject.Model.Response;
using BusinessObject.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace HELLWASHER_Controller.Controllers
{
    [EnableCors("AllowAll")]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [Authorize(Roles = "Admin, Staff")]
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDTO userDTO)
        {
            var result = await _userService.CreateUser(userDTO);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpGet("customers")]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var result = await _userService.GetAllCustomers();
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }
        [HttpGet("Users")]
        [Authorize(Roles = "Admin, Customer, Staff")]
        public async Task<IActionResult> GetUsers(int page = 1, int pageSize = 10,
            string search = "", string sort = "")
        {
            var result = await _userService.GetAllUsers(page, pageSize, search, sort);
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
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _userService.GetUserById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result.Message);
            }
        }
        [HttpGet("Login/{email}&{pass}")]
        public async Task<IActionResult> Login(string email, string pass)
        {
            var result = await _userService.LoginUser(email, pass);
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
        public async Task<IActionResult> UpdateUser(int id, UpdateUserDTO userDTO)
        {
            var result = await _userService.UpdateUser(id, userDTO);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result.Message);
            }
        }
        [Authorize(Roles = "Admin, Staff")]

        [HttpPut("ChangeStatus/{id}&{status}")]
        public async Task<IActionResult> ChangeStatus(int id, string status)
        {
            var result = await _userService.ChangeStatus(id, status);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result.Message);
            }
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteUser(id);
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
