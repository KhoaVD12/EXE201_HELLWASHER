using AutoMapper;
using BusinessObject.IService;
using BusinessObject.Model.Request.CreateRequest;
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
        private readonly IProductCheckoutRepo _repo;
        private readonly IBaseRepo<Product> _productBaseRepo;
        private readonly IOrderRepo _orderRepo;
        private readonly IMapper _mapper;
        public ProductCheckoutService(IProductCheckoutRepo repo, IBaseRepo<Product> productBaserepo, IMapper mapper, IOrderRepo orderRepo)
        {
            _repo = repo;
            _productBaseRepo = productBaserepo;
            _mapper = mapper;
            _orderRepo = orderRepo;
        }

        public async Task<ServiceResponse<ProductCheckoutResponse>> CreateProductCheckout(int orderId, ProductCheckoutDTO productCheckoutDTO)
        {
            var response = new ServiceResponse<ProductCheckoutResponse>();
            try
            {
                // Check if the ProductCheckout already exists
                var existingProductCheckout = await _repo.FirstOrDefaultAsync(pc => pc.ProductId == productCheckoutDTO.ProductId && pc.OrderId == orderId);
                if (existingProductCheckout != null)
                {
                    response.Success = false;
                    response.Message = "ProductCheckout already exists.";
                    return response;
                }
                if (productCheckoutDTO.QuantityPerProduct > existingProductCheckout.QuantityPerProduct)
                {
                    response.Success = false;
                    response.Message = "Exceed Quantity ?.";
                    return response;
                }
                var productCheckout = _mapper.Map<ProductCheckout>(productCheckoutDTO);

                var product = await _productBaseRepo.GetByIdAsync(productCheckoutDTO.ProductId);

                product.Quantity -= productCheckoutDTO.QuantityPerProduct;
                productCheckout.TotalPricePerProduct = product.Price * productCheckoutDTO.QuantityPerProduct;
                productCheckout.OrderId = orderId;

                await _productBaseRepo.UpdateAsync(product);
                var order = await _orderRepo.GetByIdAsync(orderId);
                if (order == null)
                {
                    response.Success = false;
                    response.Message = "No Order like that: "+orderId;
                    return response;
                }
                order.TotalPrice += productCheckout.TotalPricePerProduct;
                await _repo.AddAsync(productCheckout);
                await _orderRepo.Update(order);
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
                var productCheckout = await _repo.GetByIdAsync(id);
                if (productCheckout == null)
                {
                    response.Success = false;
                    response.Message = "Product checkout not found";
                    return response;
                }

                var product = await _productBaseRepo.GetByIdAsync(productCheckout.ProductId);
                product.Quantity += productCheckout.QuantityPerProduct;

                await _productBaseRepo.UpdateAsync(product);
                await _repo.DeleteAsync(id);
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

        public async Task<ServiceResponse<ResponseProductCheckoutSummaryDTO>> GetCheckoutByOrderId(int id)
        {
            var res = new ServiceResponse<ResponseProductCheckoutSummaryDTO>();
            try
            {
                var checkouts = await _repo.GetAll();
                if (checkouts.Any(s => s.OrderId == id))
                {
                    checkouts = checkouts.Where(s => s.OrderId == id).ToList();
                    var list = _mapper.Map<IEnumerable<ResponseProductCheckoutDTO>>(checkouts);
                    var totalAmount = checkouts.Sum(s => s.TotalPricePerProduct);

                    var summary = new ResponseProductCheckoutSummaryDTO
                    {
                        Services = list,
                        TotalAmount = totalAmount
                    };

                    res.Success = true;
                    res.Data = summary;
                    res.Message = "Get Item Successfully";
                    return res;
                }
                else
                {
                    res.Success = false;
                    res.Message = "No Item with this OrderId";
                    return res;
                }
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"Fail to get items: {ex.Message}";
                return res;
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


        //        var exist = await _repo.GetByIdAsync(id);
        //        if (exist == null)
        //        {
        //            res.Success = false;
        //            res.Message = "No Item found with this ID";
        //            return res;
        //        }


        //        var service = await _repo.GetByIdAsync(exist.ProductId);
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
                var productCheckout = await _repo.GetByIdAsync(id);
                if (productCheckout == null)
                {
                    response.Success = false;
                    response.Message = "Product checkout not found";
                    return response;
                }
                var product = await _productBaseRepo.GetByIdAsync(productCheckout.ProductId);

                productCheckout.QuantityPerProduct = quantity;

                productCheckout.TotalPricePerProduct = product.Price * quantity;

                product.Quantity += productCheckout.QuantityPerProduct - quantity;

                await _productBaseRepo.UpdateAsync(product);
                await _repo.UpdateAsync(productCheckout);

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