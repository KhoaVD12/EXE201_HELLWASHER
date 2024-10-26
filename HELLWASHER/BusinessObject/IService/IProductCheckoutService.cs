using BusinessObject.ViewModels.ProductCheckoutDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.IService
{
    public interface IProductCheckoutService
    {
        Task<ServiceResponse<ProductCheckoutResponse>> CreateProductCheckout(int orderId, ProductCheckoutDTO productCheckoutDTO);
        Task<ServiceResponse<ProductCheckoutDTO>> UpdateProductCheckout(int id, int quantity);
        Task<ServiceResponse<bool>> DeleteProductCheckout(int id);
    }
}
