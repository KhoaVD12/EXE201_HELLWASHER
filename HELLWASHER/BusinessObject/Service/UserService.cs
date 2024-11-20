using AutoMapper;
using BusinessObject.IService;
using BusinessObject.Model.Request.CreateRequest;
using BusinessObject.Model.Request.UpdateRequest.Entity;
using BusinessObject.Model.Response;
using BusinessObject.Model.Response.Login_SignUp;
using BusinessObject.Utils;
using DataAccess.BaseRepo;
using DataAccess.Entity;
using DataAccess.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Service
{
    public class UserService:IUserService
    {
        private readonly IBaseRepo<User> _repo;
        private readonly IMapper _mapper;
        public UserService(IBaseRepo<User> repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<ServiceResponse<ResponseUserDTO>> ChangeStatus(int id, string status)
        {
            var res = new ServiceResponse<ResponseUserDTO>();
            try
            {
                var exist = await _repo.GetByIdAsync(id);
                if (exist == null)
                {
                    res.Success = false;
                    res.Message = "No user found";
                    return res;
                }
                else
                {
                    if (UserEnum.Active.ToString().Equals(status, StringComparison.OrdinalIgnoreCase))
                    {
                        exist.Status = UserEnum.Active;
                    }
                    else if (UserEnum.Inactive.ToString().Equals(status, StringComparison.OrdinalIgnoreCase))
                    {
                        exist.Status = UserEnum.Inactive;
                    }
                    else
                    {
                        res.Success = false;
                        res.Message = "Invalid Status";
                        return res;
                    }
                    await _repo.UpdateAsync(exist);
                    var mapp = _mapper.Map<ResponseUserDTO>(exist);
                    res.Success = true;
                    res.Message = "Update user Successfully";
                    res.Data = mapp;
                    return res;
                }
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"Fail to change Status:{ex.Message}";
                return res;
            }
        }

        public async Task<ServiceResponse<ResponseUserDTO>> CreateUser(CreateUserDTO userDTO)
        {
            var res = new ServiceResponse<ResponseUserDTO>();
            try
            {
                var exist = await _repo.GetAllAsync();
                if (exist.Any(u=>u.Name==userDTO.Name||u.Phone==userDTO.Phone))
                {
                    res.Success = false;
                    res.Message = "Name/Phone existed";
                    return res;
                }
                else
                {
                    var mapp = _mapper.Map<User>(userDTO);
                    mapp.Status = UserEnum.Active;
                    mapp.Role = "Customer";
                    await _repo.AddAsync(mapp);
                    var result = _mapper.Map<ResponseUserDTO>(mapp);
                    res.Success = true;
                    res.Message = "Update user Successfully";
                    res.Data = result;
                    return res;
                }
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"Fail to update user:{ex.Message}";
                return res;
            }
        }

        public async Task<ServiceResponse<bool>> DeleteUser(int id)
        {
            var res = new ServiceResponse<bool>();
            try
            {
                var exist = await _repo.GetByIdAsync(id);
                if (exist == null)
                {
                    res.Success = false;
                    res.Message = "No user found";
                    return res;
                }
                else
                {
                    await _repo.DeleteAsync(id);
                    res.Success = true;
                    res.Message = "Delete user Successfully";
                    return res;
                }
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"Fail to delete user:{ex.Message}";
                return res;
            }
        }

        public async Task<ServiceResponse<PaginationModel<ResponseUserDTO>>> GetAllUsers(int page, int pageSize, string? search, string sort)
        {
            var res = new ServiceResponse<PaginationModel<ResponseUserDTO>>();
            try
            {
                var services = await _repo.GetAllAsync();
                if (!string.IsNullOrEmpty(search))
                {
                    services = services.Where(e => e.Name.Contains(search, StringComparison.OrdinalIgnoreCase)||
                    e.Phone.Contains(search, StringComparison.OrdinalIgnoreCase));

                }
                services = sort.ToLower().Trim() switch
                {
                    "name" => services.OrderBy(e => e.Name),
                    "phone" => services.OrderBy(e => e.Phone),
                    _ => services.OrderBy(e => e.UserId)
                };
                var mapp = _mapper.Map<IEnumerable<ResponseUserDTO>>(services);
                if (mapp.Any())
                {
                    var paginationModel = await Pagination.GetPaginationEnum(mapp, page, pageSize);
                    res.Success = true;
                    res.Message = "Get Users successfully";
                    res.Data = paginationModel;
                    return res;
                }
                else
                {
                    res.Success = false;
                    res.Message = "No User";
                    return res;
                }
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"Fail to get User:{ex.Message}";
                return res;
            }
        }

        public async Task<ServiceResponse<ResponseUserDTO>> GetUserById(int id)
        {
            var res = new ServiceResponse<ResponseUserDTO>();
            try
            {
                var exist = await _repo.GetByIdAsync(id);
                if (exist == null)
                {
                    res.Success = false;
                    res.Message = "No user found";
                    return res;
                }
                else
                {
                    var mapp = _mapper.Map<ResponseUserDTO>(exist);
                    res.Success = true;
                    res.Message = "Get user Successfully";
                    res.Data = mapp;
                    return res;
                }
            }
            catch(Exception ex)
            {
                res.Success = false;
                res.Message = $"Fail to get user:{ex.Message}";
                return res;
            }
        }

        public async Task<ServiceResponse<LoginRes>> LoginUser(string email, string pass)
        {
            var res= new ServiceResponse<LoginRes>();
            try
            {
                var accounts = await _repo.GetAllAsync();
                if(accounts.Any(u=>u.Email==email&&u.Password==pass&&u.Role=="customer"&&
                u.Status == UserEnum.Active))
                {
                    var account = accounts.Where(u => u.Email == email && u.Password == pass).FirstOrDefault();
                    var mapp=_mapper.Map<LoginRes>(account);
                    res.Success = true;
                    res.Data= mapp;
                    res.Message = "Login Successfully";
                    return res;
                }
                else
                {
                    res.Success = true;
                    res.Message = "Login Fail";
                    return res;
                }
            }
            catch(Exception ex)
            {
                res.Success = true;
                res.Message = $"Fail to login:{ex.Message}";
                return res;
            }
        }

        public async Task<ServiceResponse<ResponseUserDTO>> UpdateUser(int id, UpdateUserDTO userDTO)
        {
            var res = new ServiceResponse<ResponseUserDTO>();
            try
            {
                var exist = await _repo.GetByIdAsync(id);
                if (exist == null)
                {
                    res.Success = false;
                    res.Message = "No user found";
                    return res;
                }
                else
                {
                    exist.Name = userDTO.Name;
                    exist.Email = userDTO.Email;
                    exist.Password = userDTO.Password;
                    exist.Phone = exist.Phone;

                    if (RoleEnum.Admin.ToString().Equals(userDTO.Role, StringComparison.OrdinalIgnoreCase))
                    {
                        exist.Role = "Admin";
                    }
                    else if (RoleEnum.Customer.ToString().Equals(userDTO.Role, StringComparison.OrdinalIgnoreCase))
                    {
                        exist.Role = "Customer";
                    }
                    else
                    {
                        res.Success = false;
                        res.Message = "Invalid Role";
                        return res;
                    }
                    if (UserEnum.Active.ToString().Equals(userDTO.Status, StringComparison.OrdinalIgnoreCase))
                    {
                        exist.Status = UserEnum.Active;
                    }
                    else if (UserEnum.Inactive.ToString().Equals(userDTO.Status, StringComparison.OrdinalIgnoreCase))
                    {
                        exist.Status = UserEnum.Inactive;
                    }
                    else
                    {
                        res.Success = false;
                        res.Message = "Invalid Status";
                        return res;
                    }
                    await _repo.UpdateAsync(exist);
                    var mapp=_mapper.Map<ResponseUserDTO>(exist);
                    res.Success = true;
                    res.Message = "Update user Successfully";
                    res.Data = mapp;
                    return res;
                }
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"Fail to update user:{ex.Message}";
                return res;
            }
        }
        public async Task<User> GetUserByTokenAsync(ClaimsPrincipal claims)
        {
            if (claims == null || claims.Identity.IsAuthenticated == false)
            {
                return null;
                throw new ArgumentNullException("Invalid token");
                
            }
            var userId = claims.FindFirst("Id")?.Value;
            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int id))
            {
                throw new ArgumentException("No user can be found");
            }

            var user = await _repo.GetByIdAsync(id);
            if (user == null)
            {
                throw new NullReferenceException("No user can be found");
            }
            return user;
        }
    }
}
