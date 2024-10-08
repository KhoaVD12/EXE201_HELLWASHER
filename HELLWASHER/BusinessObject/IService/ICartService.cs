using BusinessObject.Model.Request.CreateRequest;
using BusinessObject.Model.Request.UpdateRequest.Entity;
using BusinessObject.Model.Request.UpdateRequest.Status;
using BusinessObject.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.IService
{
    public interface ICartService
    {
        Task<ServiceResponse<IEnumerable<ResponseCartDTO>>> GetCarts();
        Task<ServiceResponse<ResponseCartDTO>> CreateCart(CreateCartDTO cartDTO);
        Task<ServiceResponse<ResponseCartDTO>> ChangeCartStatus(int id, string status);
        Task<ServiceResponse<ResponseCartDTO>> UpdateCartTotalPrice(int id);
    }
}
