using Net.payOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Net.payOS.Types;
using DataAccess.IRepo;
using AutoMapper;
using DataAccess.Entity;
using DataAccess.BaseRepo;
namespace BusinessObject.Utils.PayOs
{
    public class PayOsService:IPayOsService
    {
        private readonly PayOS _payOS;
        private readonly IOrderRepo _orderRepo;
        private readonly IMapper _mapper;
        private readonly IBaseRepo<Payment> _paymentRepo;
        public PayOsService(PayOS payOS, IOrderRepo orderRepo, IMapper mapper, IBaseRepo<Payment> paymentRepo)
        {
            _payOS = payOS;
            _orderRepo = orderRepo;
            _mapper = mapper;
            _paymentRepo = paymentRepo;
        }
        #region Map Item
        // Helper method to combine ProductCheckout and ServiceCheckout into ItemData
        private static List<ItemData> MapItems(Order order)
        {
            var items = new List<ItemData>();

            if (order.ProductCheckouts != null)
            {
                items.AddRange(order.ProductCheckouts.Select(pc => new ItemData(
                    pc.Product.Name, // Assuming ProductName exists
                    pc.QuantityPerProduct,
                    (int)pc.TotalPricePerProduct
                )));
            }

            if (order.ServiceCheckouts != null)
            {
                items.AddRange(order.ServiceCheckouts.Select(sc => new ItemData(
                    sc.Service.Name, // Assuming ServiceName exists
                    1, // Services typically don’t have quantity
                    (int)sc.TotalPricePerService
                )));
            }

            return items;
        }
        #endregion
        public async Task<ServiceResponse<CreatePaymentResult>> CreatePaymentAsync(int orderId, string? returnUrl="http://localhost:5295", string? cancelUrl= "http://localhost:5295")
        {
            var res=new ServiceResponse<CreatePaymentResult>();
            try
            {
                var order=await _orderRepo.GetOrderWithDetails(orderId);
                if (order == null||(order.ServiceCheckouts.Count == 0 && order.ProductCheckouts.Count == 0))
                {
                    res.Success = false;
                    res.Message = "Order not found";
                    return res;
                }

                // Map the order to PaymentData
                // Manually map Order to PaymentData
                var paymentData = new PaymentData(
                    orderCode: DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                    amount: (int)order.TotalPrice,
                    description: $"Payment for Order {order.OrderId}",
                    items: MapItems(order),
                    cancelUrl: cancelUrl,
                    returnUrl: returnUrl,
                    buyerName: order.CustomerName,
                    buyerEmail: order.CustomerEmail,
                    buyerPhone: order.CusomterPhone,
                    buyerAddress: order.Address
                );
                var paymentResult = await _payOS.createPaymentLink(paymentData);
                if (paymentResult != null)
                {
                    var payment = new Payment
                    {
                        Bin=paymentResult.bin,
                        AccountNumber=paymentResult.accountNumber,
                        Amount=paymentResult.amount,
                        Description=paymentResult.description,
                        OrderId=orderId,
                        Currency=paymentResult.currency,
                        PaymentLinkId=paymentResult.paymentLinkId,
                        Status=paymentResult.status,
                        ExpiredAt=paymentResult.expiredAt,
                        CheckoutUrl=paymentResult.checkoutUrl,
                        QrCode=paymentResult.qrCode,
                    };
                    await _paymentRepo.AddAsync(payment);
                    res.Success = true;
                    res.Message = "Payment created successfully";
                    res.Data = paymentResult;

                    // Optionally, save the payment link to the database
                    return res;
                }
                else
                {
                    res.Success = false;
                    res.Message = "Failed to create payment";
                    return res;
                }
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message= $"Got Error: {ex.Message}";
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
