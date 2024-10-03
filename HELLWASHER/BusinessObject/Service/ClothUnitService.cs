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
    public class ClothUnitService : IClothUnitService
    {
        private readonly IBaseRepo<ClothUnit> _baseRepo;
        private readonly IMapper _mapper;
        public ClothUnitService(IBaseRepo<ClothUnit> repo, IMapper mapper)
        {
            _baseRepo = repo;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<ResponseClothUnitDTO>> CreateClothUnit(CreateClothUnitDTO clothUnitDTO)
        {
            var res = new ServiceResponse<ResponseClothUnitDTO>();
            try
            {
                var existList = await _baseRepo.GetAllAsync();
                if (existList.Any(s => s.Name == clothUnitDTO.Name))
                {
                    res.Success = false;
                    res.Message = "Name existed";
                    return res;
                }
                var mapp = _mapper.Map<ClothUnit>(clothUnitDTO);
                await _baseRepo.AddAsync(mapp);
                var result = _mapper.Map<ResponseClothUnitDTO>(mapp);
                res.Success = true;
                res.Data = result;
                res.Message = "Cloth Unit Created Successfully";
                return res;
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"Fail to create Cloth Unit:{ex.Message}";
                return res;
            }
        }

        public async Task<ServiceResponse<bool>> DeleteClothUnit(int id)
        {
            var res = new ServiceResponse<bool>();
            try
            {
                var exist = await _baseRepo.GetByIdAsync(id);
                if (exist != null)
                {
                    await _baseRepo.DeleteAsync(id);
                    res.Success = true;
                    res.Message = "Delete Cloth Unit Successfully";
                    return res;
                }
                else
                {
                    res.Success = false;
                    res.Message = "Cloth Unit not found";
                    return res;
                }
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"Fail to delete Cloth Unit: {ex.Message}";
                return res;
            }
        }

        public async Task<ServiceResponse<PaginationModel<ResponseClothUnitDTO>>> GetAllClothUnit(int page, int pageSize, string? search, string sort)
        {
            var res = new ServiceResponse<PaginationModel<ResponseClothUnitDTO>>();
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
                    _ => services.OrderBy(e => e.ClothUnitId)
                };
                var mapp = _mapper.Map<IEnumerable<ResponseClothUnitDTO>>(services);
                if (mapp.Any())
                {
                    var paginationModel = await Pagination.GetPaginationEnum(mapp, page, pageSize);
                    res.Success = true;
                    res.Message = "Get Cloth Unit successfully";
                    res.Data = paginationModel;
                    return res;
                }
                else
                {
                    res.Success = false;
                    res.Message = "No Cloth Unit";
                    return res;
                }
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"Fail to get Cloth Unit:{ex.Message}";
                return res;
            }
        }

        public async Task<ServiceResponse<ResponseClothUnitDTO>> UpdateClothUnit(int id, ResponseClothUnitDTO clothUnitDTO)
        {
            var res = new ServiceResponse<ResponseClothUnitDTO>();
            try
            {
                var exist = await _baseRepo.GetByIdAsync(id);
                if (exist == null)
                {
                    res.Success = false;
                    res.Message = "No Cloth Unit found";
                    return res;
                }
                else
                {
                    exist.Name = clothUnitDTO.Name;
                    await _baseRepo.UpdateAsync(exist);
                    res.Success = true;
                    res.Message = "Update Cloth Unit Successfully";
                    res.Data = clothUnitDTO;
                    return res;
                }
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"Fail to update Cloth Unit:{ex.Message}";
                return res;
            }
        }
    }
}
