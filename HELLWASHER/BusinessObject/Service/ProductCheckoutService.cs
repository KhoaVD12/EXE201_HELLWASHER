using AutoMapper;
using BusinessObject.IService;
using BusinessObject.Model.Response;
using BusinessObject.ViewModels.ProductCheckoutDTO;
using DataAccess.BaseRepo;
using DataAccess.Entity;
using DataAccess.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Service
{
    public class ProductCheckoutService : IProductCheckoutService
    {
        private readonly IBaseRepo<ProductCheckout> _productCheckoutBaseRepo;
        private readonly IBaseRepo<Product> _productBaseRepo;
        private readonly IMapper _mapper;
        public ProductCheckoutService(IBaseRepo<ProductCheckout> productCheckoutBaseRepo, IBaseRepo<Product> productBaserepo, IMapper mapper)
        {
            _productCheckoutBaseRepo = productCheckoutBaseRepo;
            _productBaseRepo = productBaserepo;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<ProductCheckoutResponse>> CreateProductCheckout(int orderId, ProductCheckoutDTO productCheckoutDTO)
        {
            var response = new ServiceResponse<ProductCheckoutResponse>();
            try
            {
                // Check if the ProductCheckout already exists
                var existingProductCheckout = await _productCheckoutBaseRepo.FirstOrDefaultAsync(pc => pc.ProductId == productCheckoutDTO.ProductId && pc.orderId == orderId);
                if (existingProductCheckout != null)
                {
                    response.Success = false;
                    response.Message = "ProductCheckout already exists.";
                    return response;
                }

                var productCheckout = _mapper.Map<ProductCheckout>(productCheckoutDTO);

                var product = await _productBaseRepo.GetByIdAsync(productCheckoutDTO.ProductId);

                product.Quantity -= productCheckoutDTO.QuantityPerService;
                productCheckout.TotalPricePerService = product.Price * productCheckoutDTO.QuantityPerService;
                productCheckout.orderId = orderId;

                await _productBaseRepo.UpdateAsync(product);
                await _productCheckoutBaseRepo.AddAsync(productCheckout);

                response.Data = new ProductCheckoutResponse
                {
                    Id = productCheckout.ProductCheckoutId,
                    ProductCheckout = productCheckoutDTO,
                };
                response.Success = true;
                response.Message = "Product checkout created successfully";
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"{ex.Message}";
                return response;
            }
        }

        public async Task<ServiceResponse<bool>> DeleteProductCheckout(int id)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var productCheckout = await _productCheckoutBaseRepo.GetByIdAsync(id);
                if (productCheckout == null)
                {
                    response.Success = false;
                    response.Message = "Product checkout not found";
                    return response;
                }

                var product = await _productBaseRepo.GetByIdAsync(productCheckout.ProductId);
                product.Quantity += productCheckout.QuantityPerService;

                await _productBaseRepo.UpdateAsync(product);
                await _productCheckoutBaseRepo.DeleteAsync(id);
                response.Success = true;
                response.Message = "Product checkout deleted successfully";
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"{ex.Message}";
                return response;
            }
        }
        //public async Task<ServiceResponse<ProductCheckoutResponse>> UpdateClothWeight(int id, decimal weight)
        //{
        //    var res = new ServiceResponse<ProductCheckoutResponse>();

        //    try
        //    {
        //        // Validate weight
        //        if (weight <= 0)
        //        {
        //            res.Success = false;
        //            res.Message = "Weight must be greater than 0";
        //            return res;
        //        }


        //        var exist = await _productCheckoutBaseRepo.GetByIdAsync(id);
        //        if (exist == null)
        //        {
        //            res.Success = false;
        //            res.Message = "No Item found with this ID";
        //            return res;
        //        }


        //        var service = await _productCheckoutBaseRepo.GetByIdAsync(exist.ProductId);
        //        if (service == null)
        //        {
        //            res.Success = false;
        //            res.Message = "Associated service not found";
        //            return res;
        //        }


        //        exist.Weight = weight;
        //        exist.TotalPricePerService = Math.Round(weight * service.Price, 2);


        //        await _repo.UpdateAsync(exist);

        //        var result = _mapper.Map<ResponseServiceCheckoutDTO>(exist);

        //        res.Success = true;
        //        res.Data = result;
        //        res.Message = "Item updated successfully";
        //        return res;
        //    }
        //    catch (Exception ex)
        //    {
        //        res.Success = false;
        //        res.Message = $"Failed to update item: {ex.Message}";
        //        return res;
        //    }
        //}
        public async Task<ServiceResponse<ProductCheckoutDTO>> UpdateProductCheckout(int id, int quantity)
        {
            var response = new ServiceResponse<ProductCheckoutDTO>();
            try
            {
                if (quantity <= 0)
                {
                    response.Success = false;
                    response.Message = "Quantity must be greater than 0";
                    return response;
                }
                var productCheckout = await _productCheckoutBaseRepo.GetByIdAsync(id);
                if (productCheckout == null)
                {
                    response.Success = false;
                    response.Message = "Product checkout not found";
                    return response;
                }
                var product = await _productBaseRepo.GetByIdAsync(productCheckout.ProductId);

                productCheckout.QuantityPerService = quantity;

                productCheckout.TotalPricePerService = product.Price * quantity;

                product.Quantity += productCheckout.QuantityPerService - quantity;

                await _productBaseRepo.UpdateAsync(product);
                await _productCheckoutBaseRepo.UpdateAsync(productCheckout);

                response.Data = _mapper.Map<ProductCheckoutDTO>(productCheckout);
                response.Success = true;
                response.Message = "Product checkout updated successfully";
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"{ex.Message}";
                return response;
            }
        }
    }
}