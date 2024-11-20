using BusinessObject.ViewModels.Authen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.IService
{
    public interface IAuthenService
    {
        public Task<ServiceResponse<RegisterRequest>> RegisterAsync(RegisterRequest request);
        public Task<ServiceResponse<RegisterRequest>> StaffRegisterAsync(RegisterRequest request);
        public Task<TokenResponse<string>> LoginAsync(LoginRequest request);
    }
}
