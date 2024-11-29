using BusinessObject.Model.Request.CreateRequest;
using BusinessObject.Model.Request.UpdateRequest.Entity;
using BusinessObject.Model.Response;
using DataAccess.Enum;
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
        Task<ServiceResponse<ResponseWashServiceDTO>> GetById(int id);
        Task<ServiceResponse<ResponseWashServiceDTO>> UpdateWashService(int id, UpdateWashServiceDTO serviceDTO);
        Task<ServiceResponse<bool>> UpdateWashStatus(int id, ServiceEnum status);
    }
}
