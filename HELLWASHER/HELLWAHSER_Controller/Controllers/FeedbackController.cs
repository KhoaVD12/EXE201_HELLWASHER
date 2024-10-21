using BusinessObject.IService;
using BusinessObject.ViewModels.Feedback;
using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HELLWASHER_Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;
        private readonly WashShopContext _context;
        public FeedbackController(IFeedbackService feedbackService, WashShopContext context)
        {
            _feedbackService = feedbackService;
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllFeedbacks()
        {
            var result = await _feedbackService.GetAllFeedback();
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }
        [HttpGet("{feedbackId}")]
        public async Task<IActionResult> GetFeedbackById(int feedbackId)
        {
            var result = await _feedbackService.GetFeedbackById(feedbackId);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }
        [HttpPost("Add/{userId}")]
        public async Task<IActionResult> AddFeedback([FromForm] FeedbackRequest feedbackDTO, int userId)
        {
            var result = await _feedbackService.AddFeedback(feedbackDTO, userId);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }
        [HttpPut("update/{feedbackId}")]
        public async Task<IActionResult> UpdateFeedback([FromBody] FeedbackRequest feedbackRequest, int feedbackId)
        {
            var result = await _feedbackService.UpdateFeedback(feedbackRequest, feedbackId);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }
        [HttpDelete("delete/{feedbackId}")]
        public async Task<IActionResult> DeleteFeedback(int feedbackId)
        {
            var result = await _feedbackService.DeleteFeedback(feedbackId);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }
        //[HttpPatch("image")]
        //public async Task<IActionResult> UpdateFeedbackImage(int feedbackId, IFormFile image)
        //{
        //    var result = await _feedbackService.UpdateFeedbackImage(feedbackId, image);
        //    if (!result.Success) return BadRequest(result);

        //    return Ok(result);
        //}

    }
}
