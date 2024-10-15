using BusinessObject.ViewModels.OrderDTO;
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
    }
}
