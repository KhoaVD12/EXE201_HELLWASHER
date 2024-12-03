using BusinessObject.Model.Request.CreateRequest;
using BusinessObject.Model.Request.UpdateRequest.Entity;
using BusinessObject.Model.Response;
using BusinessObject.Model.Response.Login_SignUp;
using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.IService
{
    public interface IUserService
    {
        Task<ServiceResponse<PaginationModel<ResponseUserDTO>>> GetAllUsers(int page, int pageSize, string? search, string sort);
        Task<ServiceResponse<IEnumerable<ResponseUserDTO>>> GetAllCustomers();
        Task<ServiceResponse<ResponseUserDTO>> ViewProfile(ClaimsPrincipal claims);
        Task<ServiceResponse<ResponseUserDTO>>GetUserById(int id);
        Task<ServiceResponse<ResponseUserDTO>> CreateUser(CreateUserDTO userDTO);
        Task<ServiceResponse<ResponseUserDTO>> UpdateUser(int id, UpdateUserDTO userDTO);
        Task<ServiceResponse<bool>> DeleteUser(int id);
        Task<ServiceResponse<LoginRes>> LoginUser(string email, string pass);
        Task<ServiceResponse<ResponseUserDTO>> ChangeStatus(int id, string status);
        Task<User> GetUserByTokenAsync(ClaimsPrincipal claims);
    }
}
