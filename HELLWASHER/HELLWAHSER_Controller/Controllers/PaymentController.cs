﻿using BusinessObject.Utils.PayOs;
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
        [HttpGet("Payments")]
        public async Task<IActionResult> GetPayments()
        {
            var result = await _service.GetAllPayment();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
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
        [HttpGet("getPaymentLinkInformation/{orderCode}")]
        public async Task<IActionResult> GetPaymentLinkInformation(long orderCode)
        {
            var result=await _service.GetPaymentLinkInformation(orderCode);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        /// <summary>
        /// MUST USE AFTER FINISHING PAYMENT AT PAYOS WEBSITE
        /// </summary>
        /// <param name="orderCode"></param>
        /// <returns></returns>
        [HttpPut("FinishPayment")]
        public async Task<IActionResult> FinishPayment(long orderCode)
        {
            var result = await _service.FinishPayment(orderCode);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        /// <summary>
        /// Cancel a payment whenever you want
        /// </summary>
        /// <param name="orderCode"></param>
        /// <param name="cancellationReason"></param>
        /// <returns></returns>
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
