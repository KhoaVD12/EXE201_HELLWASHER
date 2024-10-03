using BusinessObject.Model.Request;
using BusinessObject.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.IService
{
    public interface IClothUnitService
    {
        Task<ServiceResponse<ResponseClothUnitDTO>> CreateClothUnit(CreateClothUnitDTO clothUnitDTO);
        Task<ServiceResponse<bool>> DeleteClothUnit(int id);
        Task<ServiceResponse<PaginationModel<ResponseClothUnitDTO>>> GetAllClothUnit(int page, int pageSize,
            string? search, string sort);
        Task<ServiceResponse<ResponseClothUnitDTO>> UpdateClothUnit(int id, ResponseClothUnitDTO clothUnitDTO);
    }
}
