using AutoMapper;
using BusinessObject.IService;
using BusinessObject.Model.Request.CreateRequest;
using BusinessObject.Model.Request.UpdateRequest.Entity;
using BusinessObject.Model.Request.UpdateRequest.Status;
using BusinessObject.Model.Response;
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
    public class CartService:ICartService
    {
        private readonly ICartRepo _repo;
        private readonly IMapper _mapper;
        private readonly IBaseRepo<CartItem> _itemRepo;
        public CartService(ICartRepo repo, IMapper mapper, IBaseRepo<CartItem> itemRepo)
        {
            _mapper = mapper;
            _repo = repo;
            _itemRepo = itemRepo;
        }

        public async Task<ServiceResponse<ResponseCartDTO>> ChangeCartStatus(int id, string status)
        {
            var res = new ServiceResponse<ResponseCartDTO>();
            try
            {
                if (status.Length == 0 ||(! status.Equals("finish", StringComparison.CurrentCultureIgnoreCase) &&! status.Equals("choosing", StringComparison.CurrentCultureIgnoreCase)))
                {
                    res.Success = false;
                    res.Message = "No kind of that status in DB";
                    return res;
                }
                else
                {
                    var exist = await _repo.GetByIdAsync(id);
                    if (exist == null)
                    {
                        res.Success = false;
                        res.Message = "Cart Not found";
                        return res;
                    }
                    if (exist.CartStatus == "FINISH")
                    {
                        res.Success = false;
                        res.Message = "Cart is finished, you can not change status";
                        return res;
                    }
                    var mapp = _mapper.Map<Cart>(exist);
                    if (status.Equals("choosing", StringComparison.CurrentCultureIgnoreCase))
                    {
                        mapp.CartStatus = CartEnum.CHOOSING.ToString();
                    }
                    if (status.Equals("finish", StringComparison.CurrentCultureIgnoreCase))
                    {
                        mapp.CartStatus = CartEnum.FINISH.ToString();
                    }
                    await _repo.UpdateAsync(mapp);
                    var result = _mapper.Map<ResponseCartDTO>(mapp);
                    res.Success = true;
                    res.Message = "Change Status Successfully";
                    res.Data = result;
                    return res;
                }
            }
            catch(Exception ex)
            {
                res.Success = false;
                res.Message = $"Fail to change status:{ex.Message}";
                return res;
            }
        }

        public async Task<ServiceResponse<ResponseCartDTO>> CreateCart(CreateCartDTO cartDTO)
        {
            var res = new ServiceResponse<ResponseCartDTO>();
            try
            {
                var mapp = _mapper.Map<Cart>(cartDTO);
                mapp.CartStatus=CartEnum.CHOOSING.ToString();
                await _repo.AddAsync(mapp);
                var result = _mapper.Map<ResponseCartDTO>(mapp);

                res.Message = "Create Carts Successfully";
                res.Data = result;
                res.Success = true;
                return res;
            }
            catch(Exception ex)
            {
                res.Message = $"Fail to Create Cart:{ex.Message}";
                res.Success = true;
                return res;
            }
        }

        public async Task<ServiceResponse<IEnumerable<ResponseCartDTO>>> GetCarts()
        {
            var res= new ServiceResponse<IEnumerable<ResponseCartDTO>>();
            try
            {
                var carts = await _repo.GetAllCartAndItem();
                var mapp = _mapper.Map<IEnumerable<ResponseCartDTO>>(carts);
                if (mapp.Any())
                {
                    /*foreach (var cart in carts)
                    {
                        var items = await _itemRepo.GetAllAsync();
                        var cartItem = items.Where(i => i.CartId == cart.CartId).ToList();
                        var itemMapp = _mapper.Map<ResponseCartItemDTO>(cartItem);
                        ResponseCartItemDTO itemRes = itemMapp;
                    }*/
                    res.Message = "Get Carts Successfully";
                    res.Data = mapp;
                    res.Success = true;
                    return res;
                }
                else
                {
                    res.Success= false;
                    res.Message = "No cart";
                    return res;
                }
            }
            catch(Exception ex)
            {
                res.Success = false;
                res.Message = $"Fail to get cart:{ex.Message}";
                return res;
            }
        }
        /// <summary>
        /// Update Price Base on total numbers of services added 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cartDTO"></param>
        /// <returns></returns>
        /// STILL ONGOING
        public async Task<ServiceResponse<ResponseCartDTO>> UpdateCartTotalPrice(int id)
        {
            var res = new ServiceResponse<ResponseCartDTO>();
            try
            {
                var exist = await _repo.GetCartAndItemInsideByCartId(id);
                if (exist == null)
                {
                    res.Success = false;
                    res.Message = "Cart Not found";
                    return res;
                }
                if (exist.CartStatus == "FINISH")
                {
                    res.Success = false;
                    res.Message = "Cart is finished, you can not reuse it";
                    return res;
                }
                var mapp = _mapper.Map<Cart>(exist);
                if (mapp.CartItems == null)
                {
                    res.Message = "No item inside";
                    res.Success = false;
                    return res;
                }
                else
                {
                    foreach (var item in exist.CartItems)
                    {
                        exist.TotalPrice += item.TotalPricePerService;
                    }
                }
                await _repo.UpdateAsync(exist);
                var result = _mapper.Map<ResponseCartDTO>(mapp);
                res.Message = "Update Cart Total Price Successfully";
                res.Data = result;
                res.Success = true;
                return res;
            }
            catch(Exception ex)
            {
                res.Message = $"Fail to Update:{ex.Message}";
                res.Success = false;
                return res;
            }
        }
    }
}
