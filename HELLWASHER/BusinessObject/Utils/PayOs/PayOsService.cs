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
using MailKit.Search;
using CloudinaryDotNet.Actions;
namespace BusinessObject.Utils.PayOs
{
    public class PayOsService:IPayOsService
    {
        private readonly PayOS _payOS;
        private readonly IOrderRepo _orderRepo;
        private readonly IMapper _mapper;
        private readonly IBaseRepo<Payment> _paymentRepo;
        private readonly IBaseRepo<DataAccess.Entity.PaymentLinkInformation> _infoRepo;
        private readonly IBaseRepo<DataAccess.Entity.Transaction> _transactionRepo;
        public PayOsService(PayOS payOS, IOrderRepo orderRepo, IMapper mapper, IBaseRepo<Payment> paymentRepo,
            IBaseRepo<DataAccess.Entity.PaymentLinkInformation> infoRepo,
            IBaseRepo<DataAccess.Entity.Transaction> transactionRepo)
        {
            _payOS = payOS;
            _orderRepo = orderRepo;
            _mapper = mapper;
            _paymentRepo = paymentRepo;
            _infoRepo = infoRepo;
            _transactionRepo = transactionRepo;
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
                        OrderCode=paymentResult.orderCode,
                        Currency=paymentResult.currency,
                        PaymentLinkId=paymentResult.paymentLinkId,
                        Status=paymentResult.status,
                        ExpiredAt=paymentResult.expiredAt,
                        CheckoutUrl=paymentResult.checkoutUrl,
                        QrCode=paymentResult.qrCode,
                    };
                    await _paymentRepo.AddAsync(payment);
                    if (payment.OrderCode != null)
                    {
                        var result = await _payOS.getPaymentLinkInformation(payment.OrderCode);
                        var paymentLinkInfomation = new DataAccess.Entity.PaymentLinkInformation
                        {
                            Id = result.id,
                            OrderCode = payment.OrderCode,
                            Amount = result.amount,
                            AmountPaid = result.amountPaid,
                            AmountRemaining = result.amountRemaining,
                            Status = result.status,
                            CreateAt = result.createdAt,
                            CancelAt = result.canceledAt,
                            CancellationReason = result.cancellationReason
                        };
                        await _infoRepo.AddAsync(paymentLinkInfomation);
                        if (result.transactions != null)
                        {
                            var infos = await _infoRepo.GetAllAsync();
                            var exist = infos.FirstOrDefault(i => i.OrderCode == payment.OrderCode);
                            if (exist != null)
                            {
                                foreach (var item in result.transactions)
                                {
                                    var transaction = new DataAccess.Entity.Transaction
                                    {
                                        PaymentLinkInformationId = exist.PaymentLinkInformationId,
                                        Reference = item.reference,
                                        Amount = item.amount,
                                        AccountNumber = item.accountNumber,
                                        Description = item.description,
                                        TransactionDateTime = item.transactionDateTime,
                                        VirtualAccountName = item.virtualAccountName,
                                        VirtualAccountNumber = item.virtualAccountNumber,
                                        CounterAccountBankId = item.counterAccountBankId,
                                        CounterAccountBankName = item.counterAccountBankName,
                                        CounterAccountName = item.counterAccountName,
                                        CounterAccountNumber = item.counterAccountNumber
                                    };
                                    await _transactionRepo.AddAsync(transaction);
                                }
                            }
                        }
                    }

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
        public async Task<ServiceResponse<Net.payOS.Types.PaymentLinkInformation>> GetPaymentLinkInformation(long orderCode)
        {
            var res = new ServiceResponse<Net.payOS.Types.PaymentLinkInformation>();
            try
            {
                var result = await _payOS.getPaymentLinkInformation(orderCode);
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
        public async Task<ServiceResponse<Net.payOS.Types.PaymentLinkInformation>> CancelPaymentLink(long orderCode, string? cancellationReason)
        {
            var res = new ServiceResponse<Net.payOS.Types.PaymentLinkInformation>();
            try
            {
                var result = await _payOS.cancelPaymentLink(orderCode, cancellationReason);
                if (result != null)
                {
                    var payments = await _paymentRepo.GetAllAsync();
                    var exist = payments.FirstOrDefault(p => p.OrderCode == orderCode);
                    var infos=await _infoRepo.GetAllAsync();
                    var infoExist = infos.FirstOrDefault(i => i.OrderCode == orderCode);
                    if (exist != null&&infoExist!=null)
                    {
                        exist.Status = result.status;
                        infoExist.Status = result.status;
                        infoExist.CancelAt = result.canceledAt;
                        infoExist.CancellationReason= cancellationReason;
                        await _paymentRepo.UpdateAsync(exist);
                        await _infoRepo.UpdateAsync(infoExist);
                        res.Success = true;
                        res.Message = "Get Successfully";
                        res.Data = result;
                        return res;
                    }
                    else
                    {
                        res.Success = false;
                        res.Message = "No payment/payment Link Info found?";
                        return res;
                    }
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
