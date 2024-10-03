using BusinessObject.Model.Request;
using BusinessObject.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.IService
{
    public interface IWashServiceTypeService
    {
        Task<ServiceResponse<ResponseWashServiceTypeDTO>> CreateWashServiceType(CreateWashServiceTypeDTO serviceTypeDTO);
        Task<ServiceResponse<bool>> DeleteWashServiceType(int id);
        Task<ServiceResponse<PaginationModel<ResponseWashServiceTypeDTO>>> GetAllWashServiceType(int page, int pageSize,
            string? search, string sort);
        Task<ServiceResponse<ResponseWashServiceTypeDTO>> UpdateWashServiceType(int id, ResponseWashServiceTypeDTO serviceTypeDTO);
    }
}
