using AutoMapper;
using BusinessObject.IService;
using BusinessObject.Model.Request.CreateRequest;
using BusinessObject.Model.Response;
using BusinessObject.Utils;
using BusinessObject.ViewModels.ServiceDTO;
using DataAccess.BaseRepo;
using DataAccess.Entity;
using DataAccess.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Service
{
    public class WashServiceService:IWashServiceService
    {
        private readonly IBaseRepo<WashService> _baseRepo;
        private readonly IMapper _mapper;
        public WashServiceService(IBaseRepo<WashService> repo, IMapper mapper)
        {
            _baseRepo = repo;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<ResponseWashServiceDTO>> CreateWashService(CreateWashServiceDTO serviceDTO)
        {
            var res = new ServiceResponse<ResponseWashServiceDTO>();
            try
            {
                var existList = await _baseRepo.GetAllAsync();
                if(existList.Any(s=>s.Name==serviceDTO.Name))
                {
                    res.Success = false;
                    res.Message = "Name existed";
                    return res;
                }
                var mapp = _mapper.Map<WashService>(serviceDTO);
                /*mapp.ServiceStatus = 1;*/
                await _baseRepo.AddAsync(mapp);
                var result = _mapper.Map<ResponseWashServiceDTO>(mapp);
                res.Success = true;
                res.Data = result;
                res.Message = "Service Created Successfully";
                return res;
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"Fail to create Service:{ex.Message}";
                return res;
            }
        }

        public async Task<ServiceResponse<bool>> DeleteWashService(int id)
        {
            var res = new ServiceResponse<bool>();
            try
            {
                var exist= await _baseRepo.GetByIdAsync(id);
                if (exist != null)
                {
                    await _baseRepo.DeleteAsync(id);
                    res.Success = true;
                    res.Message = "Delete Service Successfully";
                    return res;
                }
                else
                {
                    res.Success = false;
                    res.Message = "Service not found";
                    return res;
                }
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"Fail to delete Service: {ex.Message}";
                return res;
            }
        }

        public async Task<ServiceResponse<PaginationModel<ResponseWashServiceDTO>>> GetAllWashService(int page, int pageSize, string? search, string sort)
        {
            var res = new ServiceResponse<PaginationModel<ResponseWashServiceDTO>>();
            try
            {
                var services = await _baseRepo.GetAllAsync();
                if (!string.IsNullOrEmpty(search))
                {
                    services = services.Where(e => e.Name.Contains(search, StringComparison.OrdinalIgnoreCase));

                }
                services = sort.ToLower().Trim() switch
                {
                    "name" => services.OrderBy(e => e.Name),
                    "price" => services.OrderBy(e => e.Price),
                    _ => services.OrderBy(e => e.WashServiceId)
                };
                var mapp = _mapper.Map<IEnumerable<ResponseWashServiceDTO>>(services);
                if (mapp.Any())
                {
                    var paginationModel = await Pagination.GetPaginationEnum(mapp, page, pageSize);
                    res.Success = true;
                    res.Message = "Get Services successfully";
                    res.Data = paginationModel;
                    return res;
                }
                else
                {
                    res.Success = false;
                    res.Message = "No Service";
                    return res;
                }
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"Fail to get Services:{ex.Message}";
                return res;
            }
        }

        public async Task<ServiceResponse<ResponseWashServiceDTO>> UpdateWashService(int id, ResponseWashServiceDTO serviceDTO)
        {
            var res = new ServiceResponse<ResponseWashServiceDTO>();
            try
            {
                var exist = await _baseRepo.GetByIdAsync(id);
                if (exist == null)
                {
                    res.Success=false;
                    res.Message = "No service found";
                    return res;
                }
                else
                {
                    exist.Name = serviceDTO.Name;
                    exist.Description = serviceDTO.Description;
                    
                    exist.ClothUnit = serviceDTO.ClothUnit;
                    exist.Price = serviceDTO.Price;
                    if (serviceDTO.ServiceStatus.ToUpper().Trim() == ServiceEnum.AVAILABLE.ToString())
                    {
                        exist.ServiceStatus = ServiceEnum.AVAILABLE;
                    }
                    else if (serviceDTO.ServiceStatus.ToUpper().Trim() == ServiceEnum.UNAVAILABLE.ToString())
                    {
                        exist.ServiceStatus = ServiceEnum.UNAVAILABLE;
                    }
                    else
                    {
                        res.Success = false;
                        res.Message = "Invalid Status";
                        return res;
                    }
                    exist.ImageURL = serviceDTO.ImageURL;
                    await _baseRepo.UpdateAsync(exist);
                    res.Success = true;
                    res.Message = "Update service Successfully";
                    res.Data=serviceDTO;
                    return res;
                }
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"Fail to update Service:{ex.Message}";
                return res;
            }
        }

        public async Task<ServiceResponse<bool>> UpdateWashStatus(int id, string status)
        {
            var res = new ServiceResponse<bool>();
            try
            {
                var exist = await _baseRepo.GetByIdAsync(id);
                if (exist == null)
                {
                    res.Success = false;
                    res.Message = "No service found";
                    return res;
                }
                else
                {
                    if (status.ToUpper().Trim() == ServiceEnum.AVAILABLE.ToString())
                    {
                        exist.ServiceStatus = ServiceEnum.AVAILABLE;
                    }
                    else if(status.ToUpper().Trim() == ServiceEnum.UNAVAILABLE.ToString())
                    {
                        exist.ServiceStatus = ServiceEnum.UNAVAILABLE;
                    }
                    else
                    {
                        res.Success = false;
                        res.Message = "Invalid Status";
                        return res;
                    }
                    await _baseRepo.UpdateAsync(exist);
                    res.Success = true;
                    res.Message ="Update Successfully";
                    return res;
                }
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"Fail to update Service:{ex.Message}";
                return res;
            }
        }
    }
}
