using BusinessObject.ViewModels.OrderDTO;
using DataAccess.Entity;
using DataAccess.Enum;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.IService
{
    public interface IOrderService
    {
        Task<ServiceResponse<IEnumerable<OrderDTO>>> GetAllOrder();
        Task<ServiceResponse<OrderDTO>> AddOrder(OrderDTO order, int userId);
        Task<ServiceResponse<UpdateOrderRequest>> UpdateOrder(UpdateOrderRequest orderRequest, int orderId);
        Task<ServiceResponse<Order>> GetOrderById(int orderId);
        Task<ServiceResponse<OrderStatusRequest>> UpdateOrderStatus(int orderId, OrderStatusEnumRequest status);
        Task<ServiceResponse<bool>> SendConfirmOrderEmail(int orderId);
        Task<ServiceResponse<bool>> AddConfirmImage(int orderId, string image);
    }
}
