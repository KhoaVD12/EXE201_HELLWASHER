using Net.payOS.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Utils.PayOs
{
    public interface IPayOsService
    {
        Task<ServiceResponse<CreatePaymentResult>> CreatePaymentAsync(int orderId, string? returnUrl = "http://localhost:5295", string? cancelUrl = "http://localhost:5295");
        Task<ServiceResponse<PaymentLinkInformation>> GetPaymentLinkInformation(int id);
        Task<ServiceResponse<PaymentLinkInformation>> CancelPaymentLink(long orderCode, string? cancellationReason);
    }
}
