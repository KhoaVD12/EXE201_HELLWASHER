using BusinessObject.Model.Request;
using BusinessObject.Model.Response;
using BusinessObject.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.IService
{
    public interface IUserService
    {
        Task<ServiceResponse<ResponseUserDTO>> CreateUser(CreateUserDTO userDTO);
        Task<ServiceResponse<PaginationModel<ResponseUserDTO>>> GetAllUser(int page, int pageSize,
            string? search, string sort);
        Task<ServiceResponse <bool>> DeleteUser(int id);
    }
}
