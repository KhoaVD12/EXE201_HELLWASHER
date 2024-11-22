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

        public async Task<TokenResponse<string>> LoginAsync(LoginRequest request)
        {
            var response = new TokenResponse<string>();
            try
            {
                // Hash the provided password
                var HashPass = HashPassWithSHA256.HashWithSHA256(request.Password);

                // Validate user credentials
                var userLogin = await _userRepo.GetUserByEmailAddressAndPasswordHash(request.Email, HashPass);
                if(userLogin == null)
                {
                    response.Success = false;
                    response.Message = "Username or password is incorrect";
                    return response;
                }

                // Generate access token
                var accessToken = await _userRepo.GetTokenByUserIdAsync(userLogin.UserId);

                // Generate refresh token
                var refreshToken = GenerateJsonWebTokenString.GenerateRefreshToken();

                var auth = userLogin.Role;
                var userId = userLogin.UserId;
                var tokenValue = userLogin.GenerateJsonWebToken(_appConfig, _appConfig.JWTSection.SecretKey, DateTime.Now);
                response.Success = true;
                response.Message = "Login successfully";
                response.AccessToken  = tokenValue;
                response.RefreshToken = refreshToken;
                response.Role = auth;
                response.HintId = userId;
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
                // Check if passwords match
                if (request.Password != request.ConFirmPassword)
                {
                    response.Success = false;
                    response.Message = "Password and Confirm Password do not match.";
                    return response;
                }

                var existEmail = await _userRepo.CheckExistedEmailAddress(request.Email);
                if (existEmail)
                {
                    response.Success = false;
                    response.Message = "Email is already existed";
                    return response;
                }

                var userRegister = _mapper.Map<User>(request);
                userRegister.Password = HashPassWithSHA256.HashWithSHA256(userRegister.Password);
                userRegister.Role = "Customer";
                userRegister.Token = Guid.NewGuid().ToString();
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
        public async Task<ServiceResponse<RegisterRequest>> StaffRegisterAsync(RegisterRequest request)
        {
            var response = new ServiceResponse<RegisterRequest>();
            try
            {
                // Check if passwords match
                if (request.Password != request.ConFirmPassword)
                {
                    response.Success = false;
                    response.Message = "Password and Confirm Password do not match.";
                    return response;
                }

                var existEmail = await _userRepo.CheckExistedEmailAddress(request.Email);
                if (existEmail)
                {
                    response.Success = false;
                    response.Message = "Email is already existed";
                    return response;
                }

                var userRegister = _mapper.Map<User>(request);
                userRegister.Password = HashPassWithSHA256.HashWithSHA256(userRegister.Password);
                userRegister.Role = "Staff";
                userRegister.Token = Guid.NewGuid().ToString();
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
