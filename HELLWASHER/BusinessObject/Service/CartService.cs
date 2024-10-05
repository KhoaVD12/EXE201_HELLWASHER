using AutoMapper;
using BusinessObject.IService;
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
    public class CartService:ICartService
    {
        private readonly IBaseRepo<Cart> _repo;
        private readonly IMapper _mapper;
        public CartService(IBaseRepo<Cart> repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<ServiceResponse<IEnumerable<ResponseCartDTO>>> GetCarts()
        {
            var res= new ServiceResponse<IEnumerable<ResponseCartDTO>>();
            try
            {
                var carts = await _repo.GetAllAsync();
                var mapp = _mapper.Map<IEnumerable<ResponseCartDTO>>(carts);
                if (mapp.Any())
                {
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
    }
}
