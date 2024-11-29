using BusinessLogicLayer;
using BusinessObject.IService;
using BusinessObject.Service;
using BusinessObject.Utils.PayOs;
using BusinessObject.ViewModels.ProductCheckoutDTO;
using DataAccess.BaseRepo;
using DataAccess.IRepo;
using DataAccess.Repo;
using Microsoft.AspNetCore.Http;
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

            services.AddScoped<IProductCheckoutRepo, ProductCheckoutRepo>();
            services.AddScoped<IServiceCheckoutRepo, ServiceCheckoutRepo>();

            services.AddScoped<IWashServiceService, WashServiceService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IProductCategoryService, ProductCategoryService>();
            services.AddScoped<IServiceCategoryService, ServiceCategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductCheckoutService, ProductCheckoutService>();

            services.AddScoped<IProductRepo, ProductRepo>();
            services.AddScoped<IOrderRepo, OrderRepo>();
            services.AddScoped<IUserRepo, UserRepo>();

            services.AddScoped<IServiceCheckoutService, ServiceCheckoutService>();

            services.AddScoped<IFeedbackService, FeedbackService>();

            services.AddScoped<IAuthenService, AuthenService>();

            services.AddScoped<IClaimService, ClaimService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IPayOsService, PayOsService>();
            return services;
        }
    }
}
