using Net.payOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Net.payOS.Types;
namespace BusinessObject.Utils.PayOs
{
    public class PayOsService:IPayOsService
    {
        private readonly PayOS _payOS;
        public PayOsService(PayOS payOS)
        {
            _payOS = payOS;
        }
        public async Task<ServiceResponse<CreatePaymentResult>> CreatePaymentAsync(PaymentData paymentData)
        {
            var res=new ServiceResponse<CreatePaymentResult>();
            try
            {
                var result = await _payOS.createPaymentLink(paymentData);
                if (result != null)
                {
                    res.Success = true;
                    res.Message = "Create Successfully";
                    res.Data = result;
                    return res;
                }
                else
                {
                    res.Success = false;
                    res.Data = result;
                    return res;
                }
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message= ex.Message;
                return res;
            }
        }
        public async Task<ServiceResponse<PaymentLinkInformation>> GetPaymentLinkInformation(int id)
        {
            var res= new ServiceResponse<PaymentLinkInformation>();
            try
            {
                var result = await _payOS.getPaymentLinkInformation(id);
                if (result != null)
                {
                    res.Success = true;
                    res.Message = "Get Successfully";
                    res.Data = result;
                    return res;
                }
                else
                {
                    res.Success = false;
                    res.Data = result;
                    return res;
                }
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = ex.Message;
                return res;
            }
        }
        public async Task<ServiceResponse<PaymentLinkInformation>> CancelPaymentLink(long orderCode, string? cancellationReason)
        {
            var res = new ServiceResponse<PaymentLinkInformation>();
            try
            {
                var result = await _payOS.cancelPaymentLink(orderCode, cancellationReason);
                if (result != null)
                {
                    res.Success = true;
                    res.Message = "Get Successfully";
                    res.Data = result;
                    return res;
                }
                else
                {
                    res.Success = false;
                    res.Data = result;
                    return res;
                }
            }
            catch(Exception ex)
            {
                res.Success = false;
                res.Message = ex.Message;
                return res;
            }
        }
    }
}
