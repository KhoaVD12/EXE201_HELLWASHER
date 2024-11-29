using DataAccess.Entity;
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
        Task<ServiceResponse<Net.payOS.Types.PaymentLinkInformation>> GetPaymentLinkInformation(long orderCode);
        Task<ServiceResponse<Net.payOS.Types.PaymentLinkInformation>> CancelPaymentLink(long orderCode, string? cancellationReason);
        Task<ServiceResponse<bool>> FinishPayment(long orderCode);
        Task<ServiceResponse<IEnumerable<Payment>>> GetAllPayment();

    }
}
