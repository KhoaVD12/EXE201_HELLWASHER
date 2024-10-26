using BusinessObject.ViewModels.Order;
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
        Task<ServiceResponse<IEnumerable<OrderResponse>>> GetAllOrder(User user);
        Task<ServiceResponse<QuickOrderDTO>> QuickAddOrder(QuickOrderDTO orderDTO);
        Task<ServiceResponse<AddOrderResponse>> AddOrder(OrderDTO order, User user);
        Task<ServiceResponse<UpdateOrderRequest>> UpdateOrder(UpdateOrderRequest orderRequest, int orderId, User user);
        Task<ServiceResponse<OrderResponse>> GetOrderById(int orderId, User user);
        Task<ServiceResponse<OrderStatusRequest>> UpdateOrderStatus(int orderId, OrderStatusEnumRequest status);
        Task<ServiceResponse<bool>> SendConfirmOrderEmail(int orderId);
        Task<ServiceResponse<bool>> AddConfirmImage(int orderId, IFormFile image);
    }
}
