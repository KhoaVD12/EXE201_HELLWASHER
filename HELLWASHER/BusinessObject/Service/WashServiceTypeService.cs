using AutoMapper;
using BusinessObject.IService;
using BusinessObject.Model.Request;
using BusinessObject.Model.Response;
using BusinessObject.Utils;
using DataAccess.BaseRepo;
using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Service
{
    public class WashServiceTypeService : IWashServiceTypeService
    {
        private readonly IBaseRepo<WashServiceType> _baseRepo;
        private readonly IMapper _mapper;
        public WashServiceTypeService(IBaseRepo<WashServiceType> repo, IMapper mapper)
        {
            _baseRepo = repo;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<ResponseWashServiceTypeDTO>> CreateWashServiceType(CreateWashServiceTypeDTO serviceDTO)
        {
            var res = new ServiceResponse<ResponseWashServiceTypeDTO>();
            try
            {
                var existList = await _baseRepo.GetAllAsync();
                if (existList.Any(s => s.Name == serviceDTO.Name))
                {
                    res.Success = false;
                    res.Message = "Name existed";
                    return res;
                }
                var mapp = _mapper.Map<WashServiceType>(serviceDTO);
                await _baseRepo.AddAsync(mapp);
                var result = _mapper.Map<ResponseWashServiceTypeDTO>(mapp);
                res.Success = true;
                res.Data = result;
                res.Message = "Service Type Created Successfully";
                return res;
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"Fail to create Service Type:{ex.Message}";
                return res;
            }
        }

        public async Task<ServiceResponse<bool>> DeleteWashServiceType(int id)
        {
            var res = new ServiceResponse<bool>();
            try
            {
                var exist = await _baseRepo.GetByIdAsync(id);
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

        public async Task<ServiceResponse<PaginationModel<ResponseWashServiceTypeDTO>>> GetAllWashServiceType(int page, int pageSize, string? search, string sort)
        {
            var res = new ServiceResponse<PaginationModel<ResponseWashServiceTypeDTO>>();
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
                    _ => services.OrderBy(e => e.WashServiceTypeId)
                };
                var mapp = _mapper.Map<IEnumerable<ResponseWashServiceTypeDTO>>(services);
                if (mapp.Any())
                {
                    var paginationModel = await Pagination.GetPaginationEnum(mapp, page, pageSize);
                    res.Success = true;
                    res.Message = "Get Service Types successfully";
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
                res.Message = $"Fail to get Service Types:{ex.Message}";
                return res;
            }
        }

        public async Task<ServiceResponse<ResponseWashServiceTypeDTO>> UpdateWashServiceType(int id, ResponseWashServiceTypeDTO serviceDTO)
        {
            var res = new ServiceResponse<ResponseWashServiceTypeDTO>();
            try
            {
                var exist = await _baseRepo.GetByIdAsync(id);
                if (exist == null)
                {
                    res.Success = false;
                    res.Message = "No service type found";
                    return res;
                }
                else
                {
                    exist.Name = serviceDTO.Name;
                    await _baseRepo.UpdateAsync(exist);
                    res.Success = true;
                    res.Message = "Update service type Successfully";
                    res.Data = serviceDTO;
                    return res;
                }
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"Fail to update Service Type:{ex.Message}";
                return res;
            }
        }
    }
}
