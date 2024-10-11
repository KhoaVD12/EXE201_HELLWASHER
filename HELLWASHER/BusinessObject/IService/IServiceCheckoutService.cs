using BusinessObject.Model.Request.CreateRequest;
using BusinessObject.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.IService
{
    public interface ICartItemService
    {
        Task<ServiceResponse<ResponseServiceCheckoutDTO>> CreateCartItem(CreateServiceCheckoutDTO itemDTO);
        Task<ServiceResponse<ResponseServiceCheckoutDTO>> UpdateCartItemQuantity(int id, int quantity);
    }
}
