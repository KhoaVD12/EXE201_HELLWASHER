using BusinessObject.Model.Request.CreateRequest;
using BusinessObject.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.IService
{
    public interface IWashServiceService
    {
        Task<ServiceResponse<ResponseWashServiceDTO>> CreateWashService(CreateWashServiceDTO serviceDTO);
        Task<ServiceResponse<bool>> DeleteWashService(int id);
        Task<ServiceResponse<PaginationModel<ResponseWashServiceDTO>>> GetAllWashService(int page, int pageSize,
            string? search, string sort);
        Task<ServiceResponse<ResponseWashServiceDTO>> UpdateWashService(int id, ResponseWashServiceDTO serviceDTO);
    }
}
