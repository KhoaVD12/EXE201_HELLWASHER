using BusinessObject.Model.Request.CreateRequest;
using BusinessObject.Model.Request.UpdateRequest.Entity;
using BusinessObject.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.IService
{
    public interface IProductService
    {
        Task<ServiceResponse<PaginationModel<ResponseProductDTO>>> GetAllProducts(int page, int pageSize, string? search, string sort);
        Task<ServiceResponse<ResponseProductDTO>> GetProductById(int id);
        Task<ServiceResponse<ResponseProductDTO>> CreateProduct(CreateProductDTO userDTO);
        Task<ServiceResponse<ResponseProductDTO>> UpdateProduct(int id, UpdateProductDTO userDTO);
        Task<ServiceResponse<bool>> DeleteProduct(int id);
        Task<ServiceResponse<ResponseProductDTO>> ChangeProductStatus(int id, string status);
    }
}
