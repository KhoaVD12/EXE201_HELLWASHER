using BusinessObject.Utils.PayOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net.payOS.Types;

namespace HELLWASHER_Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPayOsService _service;
        public PaymentController(IPayOsService service)
        {
            _service = service;
        }
        [HttpPost("create-payment")]
        public async Task<IActionResult> CreatePayment(int orderId, string? returnUrl = "http://localhost:5295", string? cancelUrl = "http://localhost:5295")
        {
            try
            {
                var result = await _service.CreatePaymentAsync(orderId, returnUrl, cancelUrl);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet("getPaymentLinkInformation")]
        public async Task<IActionResult> GetPaymentLinkInformation(int id)
        {
            var result=await _service.GetPaymentLinkInformation(id);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPut("cancelPaymentLink")]
        public async Task<IActionResult> CancelPaymentLink(long orderCode, string? cancellationReason)
        {
            var result = await _service.CancelPaymentLink(orderCode, cancellationReason);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
