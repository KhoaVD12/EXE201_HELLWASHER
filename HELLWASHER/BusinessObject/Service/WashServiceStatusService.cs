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
    public class WashServiceStatusService:IWashServiceStatusService
    {
        private readonly IBaseRepo<WashServiceStatus> _baseRepo;
        private readonly IMapper _mapper;
        public WashServiceStatusService(IBaseRepo<WashServiceStatus> repo, IMapper mapper)
        {
            _baseRepo = repo;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<ResponseWashServiceStatusDTO>> CreateWashServiceStatus(CreateWashServiceStatusDTO serviceStatusDTO)
        {
            var res = new ServiceResponse<ResponseWashServiceStatusDTO>();
            try
            {
                var existList = await _baseRepo.GetAllAsync();
                if (existList.Any(s => s.Name == serviceStatusDTO.Name))
                {
                    res.Success = false;
                    res.Message = "Name existed";
                    return res;
                }
                var mapp = _mapper.Map<WashServiceStatus>(serviceStatusDTO);
                await _baseRepo.AddAsync(mapp);
                var result = _mapper.Map<ResponseWashServiceStatusDTO>(mapp);
                res.Success = true;
                res.Data = result;
                res.Message = "Service Status Created Successfully";
                return res;
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"Fail to create Service Status:{ex.Message}";
                return res;
            }
        }

        public async Task<ServiceResponse<bool>> DeleteWashServiceStatus(int id)
        {
            var res = new ServiceResponse<bool>();
            try
            {
                var exist = await _baseRepo.GetByIdAsync(id);
                if (exist != null)
                {
                    await _baseRepo.DeleteAsync(id);
                    res.Success = true;
                    res.Message = "Delete Service Status Successfully";
                    return res;
                }
                else
                {
                    res.Success = false;
                    res.Message = "Service Status not found";
                    return res;
                }
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"Fail to delete Service Status: {ex.Message}";
                return res;
            }
        }

        public async Task<ServiceResponse<PaginationModel<ResponseWashServiceStatusDTO>>> GetAllWashServiceStatus(int page, int pageSize, string? search, string sort)
        {
            var res = new ServiceResponse<PaginationModel<ResponseWashServiceStatusDTO>>();
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
                    _ => services.OrderBy(e => e.WashServiceStatusId)
                };
                var mapp = _mapper.Map<IEnumerable<ResponseWashServiceStatusDTO>>(services);
                if (mapp.Any())
                {
                    var paginationModel = await Pagination.GetPaginationEnum(mapp, page, pageSize);
                    res.Success = true;
                    res.Message = "Get Service Status successfully";
                    res.Data = paginationModel;
                    return res;
                }
                else
                {
                    res.Success = false;
                    res.Message = "No Service Status";
                    return res;
                }
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"Fail to get Service Status:{ex.Message}";
                return res;
            }
        }

        public async Task<ServiceResponse<ResponseWashServiceStatusDTO>> UpdateWashServiceStatus(int id, ResponseWashServiceStatusDTO serviceStatusDTO)
        {
            var res = new ServiceResponse<ResponseWashServiceStatusDTO>();
            try
            {
                var exist = await _baseRepo.GetByIdAsync(id);
                if (exist == null)
                {
                    res.Success = false;
                    res.Message = "No Service Status found";
                    return res;
                }
                else
                {
                    exist.Name = serviceStatusDTO.Name;
                    await _baseRepo.UpdateAsync(exist);
                    res.Success = true;
                    res.Message = "Update Service Status Successfully";
                    res.Data = serviceStatusDTO;
                    return res;
                }
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"Fail to update Service Status:{ex.Message}";
                return res;
            }
        }
    }
}
