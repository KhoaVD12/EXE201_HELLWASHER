using BusinessObject.Model.Request.CreateRequest;
using BusinessObject.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.IService
{
    public interface IServiceCheckoutService
    {
        Task<ServiceResponse<ResponseServiceCheckoutDTO>> CreateServiceCheckout(CreateServiceCheckoutDTO itemDTO);
        Task<ServiceResponse<ResponseServiceCheckoutSummaryDTO>> GetCheckoutByOrderId(int id);
        Task<ServiceResponse<ResponseServiceCheckoutDTO>> UpdateClothWeight(int id, decimal weight);
        Task<ServiceResponse<bool>> DeleteCheckout(int id);
    }
}
