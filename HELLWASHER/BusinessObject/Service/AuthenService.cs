using AutoMapper;
using BusinessObject.Commons;
using BusinessObject.IService;
using BusinessObject.Utils;
using BusinessObject.ViewModels.Authen;
using DataAccess.BaseRepo;
using DataAccess.Entity;
using DataAccess.Enum;
using DataAccess.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Service
{
    public class AuthenService : IAuthenService
    {
        private readonly AppConfiguration _appConfig;
        private readonly IBaseRepo<User> _userBaseRepo;
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;
        public AuthenService(IBaseRepo<User> userBaseRepo, IMapper mapper, AppConfiguration config, IUserRepo userRepo)
        {
            _userRepo = userRepo;
            _userBaseRepo = userBaseRepo;
            _mapper = mapper;
            _appConfig = config;
        }

        public async Task<ServiceResponse<LoginRequest>> LoginAsync(LoginRequest request)
        {
            var response = new ServiceResponse<LoginRequest>();
            try
            {
                var HashPass = HashPassWithSHA256.HashWithSHA256(request.Password);
                var userLogin = await _userRepo.GetUserByEmailAddressAndPasswordHash(request.UserName, HashPass);
                if(userLogin == null)
                {
                    response.Success = false;
                    response.Message = "Username or password is incorrect";
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<RegisterRequest>> RegisterAsync(RegisterRequest request)
        {
            var response = new ServiceResponse<RegisterRequest>();
            try
            {
                var existEmail = await _userRepo.CheckExistedEmailAddress(request.Email);
                if (existEmail)
                {
                    response.Success = false;
                    response.Message = "Email is already existed";
                    return response;
                }
                var userRegister = _mapper.Map<User>(request);
                userRegister.Password = HashPassWithSHA256.HashWithSHA256(userRegister.Password);
                userRegister.Role = RoleEnum.Customer;
                await _userBaseRepo.AddAsync(userRegister);
                response.Success = true;
                response.Message = "Register successfully";
                response.Data = request;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
