using AutoMapper;
using BusinessObject.IService;
using BusinessObject.Model.Request.CreateRequest;
using BusinessObject.Model.Request.UpdateRequest.Entity;
using BusinessObject.Model.Response;
using DataAccess;
using DataAccess.BaseRepo;
using DataAccess.Entity;
using DataAccess.Enum;
using DataAccess.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Service
{
    public class ProductService : IProductService
    {
        private readonly IBaseRepo<Product> _baseRepo;
        private readonly IBaseRepo<Category> _categoryRepo;
        private readonly IMapper _mapper;
        private readonly IProductRepo _productRepo;
        private readonly WashShopContext _dbcontext;
        public ProductService(IBaseRepo<Product> baseRepo, IMapper mapper, WashShopContext dbContext, IProductRepo productRepo, IBaseRepo<Category> categoryRepo)
        {
            _baseRepo = baseRepo;
            _mapper = mapper;
            _dbcontext = dbContext;
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
        }
        public Task<ServiceResponse<ResponseProductDTO>> ChangeProductStatus(int id, string status)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<ResponseProductDTO>> CreateProduct(CreateProductDTO productDTO)
        {
            var response = new ServiceResponse<ResponseProductDTO>();
            try
            {
                var product = _mapper.Map<Product>(productDTO);
                var category = await _productRepo.GetProductWithDetails(productDTO.CategoryId);
                if (product.CategoryId == null)
                {
                    response.Success = false;
                    response.Message = "Category not found";
                    return response;
                }
                product.Name = productDTO.Name;
                product.Description = productDTO.Description;
                product.Price = productDTO.Price;
                product.Quantity = productDTO.Quantity;
                product.ImageURL = productDTO.ImageURL;
                product.CategoryId = productDTO.CategoryId;
                product.IsDeleted = false;
                await _baseRepo.AddAsync(product);

                response.Data = _mapper.Map<ResponseProductDTO>(product);
                response.Message = "Product created successfully";
                response.Success = true;

                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"{ex.Message}";
                return response;
            }
        }

        public async Task<ServiceResponse<bool>> DeleteProduct(int id)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var product = await _baseRepo.GetByIdAsync(id);
                if (product == null)
                {
                    response.Success = false;
                    response.Message = "Product not found";
                    return response;
                }

                product.IsDeleted = true;
                await _baseRepo.UpdateAsync(product);
                response.Success = true;
                response.Message = "Product deleted successfully";
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"{ex.Message}";
                return response;
            }
        }

        public async Task<ServiceResponse<IEnumerable<ResponseProductDTO>>> GetAllProduct()
        {
            var response = new ServiceResponse<IEnumerable<ResponseProductDTO>>();
            try
            {
                var products = await _baseRepo.GetAllAsync();

                var productDTOs = _mapper.Map<IEnumerable<ResponseProductDTO>>(products);
                response.Data = productDTOs;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"{ex.Message}";
            }
            return response;
        }

        public async Task<ServiceResponse<ResponseProductDTO>> GetProductById(int productId)
        {
            var response = new ServiceResponse<ResponseProductDTO>();
            try
            {
                var exist = await _baseRepo.GetByIdAsync(productId);
                if (exist == null)
                {
                    response.Success = false;
                    response.Message = "Product not found";
                    return response;
                }
                var productDTO = _mapper.Map<ResponseProductDTO>(exist);
                response.Data = productDTO;
                response.Success = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"{ex.Message}";
                return response;
            }
        }
        public async Task<ServiceResponse<UpdateProductDTO>> UpdateProduct(UpdateProductDTO productDTO, int productId)
        {
            var response = new ServiceResponse<UpdateProductDTO>();
            try
            {
                var product = await _baseRepo.GetByIdAsync(productId);
                if (product == null)
                {
                    response.Success = false;
                    response.Message = "Product not found";
                    return response;
                }


                var category = await _categoryRepo.GetByIdAsync(productDTO.CategoryId);
                if (category == null)
                {
                    response.Success = false;
                    response.Message = "Category not found";
                    return response;
                }
                product.Name = productDTO.Name;
                product.Description = productDTO.Description;
                product.Price = productDTO.Price;
                product.Quantity = productDTO.Quantity;
                product.ImageURL = productDTO.ImageURL;
                product.CategoryId = productDTO.CategoryId;

                await _baseRepo.UpdateAsync(product);
                response.Success = true;
                response.Message = "Product updated successfully";
                response.Data = productDTO;
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
