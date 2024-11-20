using BusinessObject.Model.Request.CreateRequest;
using BusinessObject.Model.Request.UpdateRequest.Entity;
using BusinessObject.Model.Response;
using BusinessObject.ViewModels.Product;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.IService
{
    public interface IProductService
    {
        Task<ServiceResponse<IEnumerable<ResponseProductDTO>>> GetAllProduct();
        Task<ServiceResponse<ResponseProductDTO>> GetProductById(int productId);
        Task<ServiceResponse<CreateProductResponse>> CreateProduct(CreateProductDTO userDTO);
        Task<ServiceResponse<ImageResponse>> ProductImage(IFormFile image, int productId);
        Task<ServiceResponse<UpdateProductResponse>> UpdateProduct(UpdateProductDTO productDTO, int productId);
        Task<ServiceResponse<bool>> DeleteProduct(int id);
    }
}
