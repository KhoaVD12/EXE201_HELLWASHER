using BusinessObject.IService;
using BusinessObject.Service;
using DataAccess.BaseRepo;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddService(this IServiceCollection services, string? DatabaseConnection)
        {
            services.AddScoped(typeof(IBaseRepo<>), typeof(BaseRepo<>));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IWashServiceService, WashServiceService>();
            return services;
        }
    }
}
