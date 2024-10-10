using BusinessObject.Model.Request.CreateRequest;
using BusinessObject.Model.Request.UpdateRequest.Entity;
using BusinessObject.Model.Response;
using BusinessObject.Model.Response.Login_SignUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.IService
{
    public interface IUserService
    {
        Task<ServiceResponse<PaginationModel<ResponseUserDTO>>> GetAllUsers(int page, int pageSize, string? search, string sort);
        Task<ServiceResponse<ResponseUserDTO>>GetUserById(int id);
        Task<ServiceResponse<ResponseUserDTO>> CreateUser(CreateUserDTO userDTO);
        Task<ServiceResponse<ResponseUserDTO>> UpdateUser(int id, UpdateUserDTO userDTO);
        Task<ServiceResponse<bool>> DeleteUser(int id);
        Task<ServiceResponse<LoginRes>> LoginUser(string email, string pass);
        Task<ServiceResponse<ResponseUserDTO>> ChangeStatus(int id, string status);
    }
}
