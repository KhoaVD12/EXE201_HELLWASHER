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
        /*Task<ServiceResponse<int>> AddOrder(OrderDTO order);
        Task<ServiceResponse<UpdateOrderRequest>> UpdateOrder(UpdateOrderRequest orderRequest);*/
    }
}
