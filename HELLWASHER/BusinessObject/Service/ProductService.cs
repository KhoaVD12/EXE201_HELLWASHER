using BusinessObject.IService;
using BusinessObject.Model.Request.CreateRequest;
using BusinessObject.Model.Request.UpdateRequest.Entity;
using BusinessObject.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Service
{
    public class ProductService : IProductService
    {
        public Task<ServiceResponse<ResponseProductDTO>> ChangeProductStatus(int id, string status)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<ResponseProductDTO>> CreateProduct(CreateProductDTO userDTO)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<bool>> DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<PaginationModel<ResponseProductDTO>>> GetAllProducts(int page, int pageSize, string? search, string sort)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<ResponseProductDTO>> GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<ResponseProductDTO>> UpdateProduct(int id, UpdateProductDTO userDTO)
        {
            throw new NotImplementedException();
        }
    }
}
