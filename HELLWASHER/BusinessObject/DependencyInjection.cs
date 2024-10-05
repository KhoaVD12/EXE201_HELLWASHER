using BusinessObject.IService;
using BusinessObject.Service;
using DataAccess.BaseRepo;
using DataAccess.IRepo;
using DataAccess.Repo;
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
            services.AddScoped<IUserRepo, UserRepo>();

            services.AddScoped(typeof(IBaseRepo<>), typeof(BaseRepo<>));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IWashServiceService, WashServiceService>();
            services.AddScoped<IWashServiceTypeService, WashServiceTypeService>();
            services.AddScoped<IClothUnitService, ClothUnitService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IWashServiceStatusService, WashServiceStatusService>();
            services.AddScoped<ICartService, CartService>();
            return services;
        }
    }
}
