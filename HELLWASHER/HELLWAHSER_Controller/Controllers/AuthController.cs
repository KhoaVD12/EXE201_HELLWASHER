using BusinessObject.IService;
using BusinessObject.ViewModels.Authen;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HELLWASHER_Controller.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/authentication")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenService _authenService;
        public AuthController(IAuthenService authenService)
        {
            _authenService = authenService;
        }
        /// <summary>
        /// Register new customer account
        /// </summary>
        /// <param name="registerRequest"></param>
        /// <returns></returns>
        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            var result = await _authenService.RegisterAsync(registerRequest);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("Register-staff")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> StaffRegister(RegisterRequest registerRequest)
        {
            var result = await _authenService.StaffRegisterAsync(registerRequest);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }


        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var result = await _authenService.LoginAsync(loginRequest);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }
    }
}
