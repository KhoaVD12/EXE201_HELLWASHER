using AutoMapper;
using Azure;
using BusinessObject.IService;
using BusinessObject.Model.Request.CreateRequest;
using BusinessObject.Model.Response;
using DataAccess.BaseRepo;
using DataAccess.Entity;
using DataAccess.IRepo;
using DataAccess.Repo;
using MailKit.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Service
{
    public class ServiceCheckoutService:IServiceCheckoutService
    {
        private readonly IServiceCheckoutRepo _repo;
        private readonly IBaseRepo<DataAccess.Entity.Service> _washServiceRepo;
        private readonly IOrderRepo _orderRepo;
        private readonly IMapper _mapper;
        public ServiceCheckoutService(IServiceCheckoutRepo repo, 
            IMapper mapper,
            IBaseRepo<DataAccess.Entity.Service> washServiceRepo,
            IOrderRepo orderRepo)
        {
            _mapper = mapper;
            _repo = repo;
            _washServiceRepo = washServiceRepo;
            _orderRepo = orderRepo;
        }

        public async Task<ServiceResponse<ResponseServiceCheckoutDTO>> CreateServiceCheckout(CreateServiceCheckoutDTO itemDTO)
        {
            var res = new ServiceResponse<ResponseServiceCheckoutDTO>();
            try
            {
                var items = await _repo.GetAllAsync();
                if (items.Any(i => i.ServiceId == itemDTO.ServiceId&&i.OrderId==itemDTO.OrderId))
                {
                    res.Success = false;
                    res.Message = "You haved added this service in the Order before, now you can only update Weight inside";
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
                mapp.TotalPricePerService = serviceExist.Price * itemDTO.Weight;
                var order = await _orderRepo.GetByIdAsync(itemDTO.OrderId);
                if (order == null)
                {
                    res.Success = false;
                    res.Message = "No Order like that: " + itemDTO.OrderId;
                    return res;
                }
                order.TotalPrice += mapp.TotalPricePerService;
                await _orderRepo.Update(order);
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

        public async Task<ServiceResponse<ResponseServiceCheckoutSummaryDTO>> GetCheckoutByOrderId(int id)
        {
            var res = new ServiceResponse<ResponseServiceCheckoutSummaryDTO>();
            try
            {
                var checkouts = await _repo.GetAll();
                if (checkouts.Any(s => s.OrderId == id))
                {
                    checkouts = checkouts.Where(s => s.OrderId == id).ToList();
                    var list = _mapper.Map<IEnumerable<ResponseServiceCheckoutDTO>>(checkouts);
                    var totalAmount = checkouts.Sum(s => s.TotalPricePerService);

                    var summary = new ResponseServiceCheckoutSummaryDTO
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

        public async Task<ServiceResponse<ResponseServiceCheckoutDTO>> UpdateClothWeight(int id, decimal weight)
        {
            var res = new ServiceResponse<ResponseServiceCheckoutDTO>();

            try
            {
                // Validate weight
                if (weight <= 0)
                {
                    res.Success = false;
                    res.Message = "Weight must be greater than 0";
                    return res;
                }

                
                var exist = await _repo.GetByIdAsync(id);
                if (exist == null)
                {
                    res.Success = false;
                    res.Message = "No Item found with this ID";
                    return res;
                }

                
                var service = await _washServiceRepo.GetByIdAsync(exist.ServiceId);
                if (service == null)
                {
                    res.Success = false;
                    res.Message = "Associated service not found";
                    return res;
                }

                
                exist.Weight = weight;
                exist.TotalPricePerService = Math.Round(weight * service.Price, 2); 

               
                await _repo.UpdateAsync(exist);

                var result = _mapper.Map<ResponseServiceCheckoutDTO>(exist);

                res.Success = true;
                res.Data = result;
                res.Message = "Item updated successfully";
                return res;
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"Failed to update item: {ex.Message}";
                return res;
            }
        }
    }
}
