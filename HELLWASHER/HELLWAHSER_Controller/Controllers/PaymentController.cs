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
        public async Task<IActionResult> CreatePayment()
        {
            long orderCode = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            List<ItemData> items = new List<ItemData>
        {
            new ItemData("Mì tôm hảo hảo ly", 1, 1000)
        };
            PaymentData paymentData = new PaymentData(orderCode, 1000, "Thanh toan don hang", items, "https://localhost:3002/cancel", "https://localhost:3002/success");

            try
            {
                var result = await _service.CreatePaymentAsync(paymentData);
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
