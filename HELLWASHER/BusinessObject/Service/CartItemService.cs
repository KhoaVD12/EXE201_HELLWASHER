using AutoMapper;
using BusinessObject.IService;
using BusinessObject.Model.Request.CreateRequest;
using BusinessObject.Model.Response;
using DataAccess.BaseRepo;
using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Service
{
    public class CartItemService:ICartItemService
    {
        private readonly IBaseRepo<CartItem> _repo;
        private readonly IBaseRepo<WashService> _washServiceRepo;
        private readonly ICartService _cartService;
        private readonly IMapper _mapper;
        public CartItemService(IBaseRepo<CartItem> repo, 
            IMapper mapper,
            IBaseRepo<WashService> washServiceRepo,
            ICartService cartService)
        {
            _mapper = mapper;
            _repo = repo;
            _washServiceRepo = washServiceRepo;
            _cartService = cartService;
        }

        public async Task<ServiceResponse<ResponseCartItemDTO>> CreateCartItem(CreateCartItemDTO itemDTO)
        {
            var res = new ServiceResponse<ResponseCartItemDTO>();
            try
            {
                var items = await _repo.GetAllAsync();
                if (items.Any(i => i.ServiceId == itemDTO.ServiceId && i.CartId == itemDTO.CartId))
                {
                    res.Success = false;
                    res.Message = "You haved added this service before, now you can only update Quantity inside";
                    return res;
                }
                var serviceExist = await _washServiceRepo.GetByIdAsync(itemDTO.ServiceId);
                if (serviceExist == null)
                {
                    res.Success = false;
                    res.Message = "No service with this Id";
                    return res;
                }
                var mapp = _mapper.Map<CartItem>(itemDTO);
                mapp.TotalPricePerService = serviceExist.Price * itemDTO.QuantityPerService;
                await _repo.AddAsync(mapp);
                var result = _mapper.Map<ResponseCartItemDTO>(mapp);
                res.Success = true;
                res.Message = "Create item successfully";
                res.Data = result;
                return res;
            }
            catch(Exception ex)
            {
                res.Success = false;
                res.Message = $"Fail to create item:{ex.Message}";
                return res;
            }
        }

        public async Task<ServiceResponse<ResponseCartItemDTO>> UpdateCartItemQuantity(int id, int quantity)
        {
            var res = new ServiceResponse<ResponseCartItemDTO>();
            try
            {
                if (quantity <= 0)
                {
                    res.Success = false;
                    res.Message = "Quantity must not be less than 1";
                    return res;
                }
                var exist = await _repo.GetByIdAsync(id);
                if (exist != null)
                {
                    var service = await _washServiceRepo.GetByIdAsync(exist.ServiceId);
                    var mapp = _mapper.Map<CartItem>(exist);
                    exist.QuantityPerService = quantity;
                    exist.TotalPricePerService = quantity * service.Price;
                    await _repo.UpdateAsync(exist);
                    await _cartService.UpdateCartTotalPrice(exist.CartId);
                    var result = _mapper.Map<ResponseCartItemDTO>(exist);
                    res.Success = true;
                    res.Data = result;
                    res.Message = "Update Item Successfully";
                    return res;
                }
                else
                {
                    res.Success = false;
                    res.Message = "No Item with this Id";
                    return res;
                }
            }
            catch(Exception ex)
            {
                res.Success = false;
                res.Message = $"Fail to update item:{ex.Message}";
                return res;
            }
        }
    }
}
