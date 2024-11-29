using BusinessObject.IService;
using BusinessObject.ViewModels.Feedback;
using DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HELLWASHER_Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;
        private readonly WashShopContext _context;
        private readonly IUserService _userService;

        public FeedbackController(IFeedbackService feedbackService, WashShopContext context, IUserService userService)
        {
            _feedbackService = feedbackService;
            _context = context;
            _userService = userService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Staff, Customer")]
        public async Task<IActionResult> GetAllFeedbacks()
        {
            var user = await _userService.GetUserByTokenAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var result = await _feedbackService.GetAllFeedback();
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("{feedbackId}")]
        [Authorize(Roles = "Admin, Staff, Customer")]
        public async Task<IActionResult> GetFeedbackById(int feedbackId)
        {
            var user = await _userService.GetUserByTokenAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var result = await _feedbackService.GetFeedbackById(feedbackId);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("Add")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> AddFeedback([FromForm] FeedbackRequest feedbackDTO)
        {
            var user = await _userService.GetUserByTokenAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var result = await _feedbackService.AddFeedback(feedbackDTO, user.UserId);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("update/{feedbackId}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> UpdateFeedback([FromForm] FeedbackRequest feedbackRequest, int feedbackId)
        {
            var user = await _userService.GetUserByTokenAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var result = await _feedbackService.UpdateFeedback(feedbackRequest, feedbackId);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("delete/{feedbackId}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> DeleteFeedback(int feedbackId)
        {
            var user = await _userService.GetUserByTokenAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var result = await _feedbackService.DeleteFeedback(feedbackId);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }
    }
}
