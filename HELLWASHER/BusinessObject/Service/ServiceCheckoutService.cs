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
    public class ServiceCheckoutService:IServiceCheckoutService
    {
        private readonly IBaseRepo<ServiceCheckout> _repo;
        private readonly IBaseRepo<WashService> _washServiceRepo;
        
        private readonly IMapper _mapper;
        public ServiceCheckoutService(IBaseRepo<ServiceCheckout> repo, 
            IMapper mapper,
            IBaseRepo<WashService> washServiceRepo
            )
        {
            _mapper = mapper;
            _repo = repo;
            _washServiceRepo = washServiceRepo;
            
        }

        public async Task<ServiceResponse<ResponseServiceCheckoutDTO>> CreateServiceCheckout(CreateServiceCheckoutDTO itemDTO)
        {
            var res = new ServiceResponse<ResponseServiceCheckoutDTO>();
            try
            {
                var items = await _repo.GetAllAsync();
                if (items.Any(i => i.ServiceId == itemDTO.ServiceId))
                {
                    res.Success = false;
                    res.Message = "You haved added this service before, now you can only update Weight inside";
                    return res;
                }
                var serviceExist = await _washServiceRepo.GetByIdAsync(itemDTO.ServiceId);
                if (serviceExist == null)
                {
                    res.Success = false;
                    res.Message = "No service with this Id";
                    return res;
                }
                var mapp = _mapper.Map<ServiceCheckout>(itemDTO);
                mapp.TotalPricePerService = serviceExist.Price * itemDTO.QuantityPerService;
                await _repo.AddAsync(mapp);
                var result = _mapper.Map<ResponseServiceCheckoutDTO>(mapp);
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

        public async Task<ServiceResponse<bool>> DeleteCheckout(int id)
        {
            var res = new ServiceResponse<bool>();
            try
            {
                
                var exist = await _repo.GetByIdAsync(id);
                if (exist != null)
                {
                    await _repo.DeleteAsync(id);
                    res.Success = true;
                    res.Message = "Delete Item Successfully";
                    return res;
                }
                else
                {
                    res.Success = false;
                    res.Message = "No Item with this Id";
                    return res;
                }
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"Fail to Delete item:{ex.Message}";
                return res;
            }
        }

        public async Task<ServiceResponse<IEnumerable<ResponseServiceCheckoutDTO>>> GetCheckoutByOrderId(int id)
        {
            var res = new ServiceResponse<IEnumerable<ResponseServiceCheckoutDTO>>();
            try
            {

                var checkouts = await _repo.GetAllAsync();
                if (checkouts.Any(s=>s.OrderId==id))
                {
                    checkouts=checkouts.Where(s=>s.OrderId==id).ToList();
                    var list = _mapper.Map<IEnumerable<ResponseServiceCheckoutDTO>>(checkouts);
                    res.Success = true;
                    res.Data=list;
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
                res.Message = $"Fail to Delete item:{ex.Message}";
                return res;
            }
        }

        public async Task<ServiceResponse<ResponseServiceCheckoutDTO>> UpdateClothWeight(int id, int weight)
        {
            var res = new ServiceResponse<ResponseServiceCheckoutDTO>();
            try
            {
                if (weight <= 0)
                {
                    res.Success = false;
                    res.Message = "Weight must not be less than 1";
                    return res;
                }
                var exist = await _repo.GetByIdAsync(id);
                if (exist != null)
                {
                    var service = await _washServiceRepo.GetByIdAsync(exist.ServiceId);
                    var mapp = _mapper.Map<ServiceCheckout>(exist);
                    exist.QuantityPerService = weight;
                    exist.TotalPricePerService = weight * service.Price;
                    await _repo.UpdateAsync(exist);
                    
                    var result = _mapper.Map<ResponseServiceCheckoutDTO>(exist);
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
