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
    public class UserService:IUserService
    {
        private readonly IBaseRepo<User> _userRepo;
        private readonly IMapper _mapper;
        public UserService(IBaseRepo<User> userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<ResponseUserDTO>> CreateUser(CreateUserDTO userDTO)
        {
            var res = new ServiceResponse<ResponseUserDTO>();
            try
            {
                var users=await _userRepo.GetAllAsync();
                
                if (users.Any(e=>e.Name==userDTO.Name||e.Email==userDTO.Email||e.Phone==userDTO.Phone))
                {
                    res.Success = false;
                    res.Message = "Name/Email/Phone existed";
                    return res;
                }
                var mapp = _mapper.Map<User>(userDTO);
                mapp.Status = "Active";
                mapp.AccountConfirm = false;
                mapp.Token = "Super Admin";
                await _userRepo.AddAsync(mapp);
                var result=_mapper.Map<ResponseUserDTO>(mapp);
                res.Success=true;
                res.Data = result;
                res.Message = "User Created Successfully";
                return res;
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"Fail to create User:{ex.Message}";
                return res;
            }
        }

        public async Task<ServiceResponse<bool>> DeleteUser(int id)
        {
            var res= new ServiceResponse<bool>();
            try
            {
                var exist = await _userRepo.GetByIdAsync(id);
                if (exist != null)
                {
                    await _userRepo.DeleteAsync(id);
                    res.Success = true;
                    res.Message = "Delete User Successfully";
                    return res;
                }
                else
                {
                    res.Success = false;
                    res.Message = "User not found";
                    return res;
                }
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"Fail to delete User: {ex.Message}";
                return res;
            }
        }

        public async Task<ServiceResponse<PaginationModel<ResponseUserDTO>>> GetAllUser(int page, int pageSize,
            string? search, string sort)
        {
            var res= new ServiceResponse<PaginationModel<ResponseUserDTO>>();
            try
            {
                var users = await _userRepo.GetAllAsync();
                if (!string.IsNullOrEmpty(search))
                {
                    users = users.Where(e => e.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    e.Email.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    e.Phone.Contains(search, StringComparison.OrdinalIgnoreCase));
                }
                users = sort.ToLower().Trim() switch
                {
                    "name" => users.OrderBy(e => e.Name),
                    "status" => users.OrderBy(e => e.Status),
                    _ => users.OrderBy(e => e.UserId)
                };
                var mapp = _mapper.Map<IEnumerable<ResponseUserDTO>>(users);
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
    }
}
