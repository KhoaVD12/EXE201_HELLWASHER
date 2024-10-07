using BusinessObject.Model.Request.CreateRequest;
using BusinessObject.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.IService
{
    public interface IWashServiceStatusService
    {
        Task<ServiceResponse<ResponseWashServiceStatusDTO>> CreateWashServiceStatus(CreateWashServiceStatusDTO serviceStatusDTO);
        Task<ServiceResponse<bool>> DeleteWashServiceStatus(int id);
        Task<ServiceResponse<PaginationModel<ResponseWashServiceStatusDTO>>> GetAllWashServiceStatus(int page, int pageSize,
            string? search, string sort);
        Task<ServiceResponse<ResponseWashServiceStatusDTO>> UpdateWashServiceStatus(int id, ResponseWashServiceStatusDTO serviceStatusDTO);
    }
}
