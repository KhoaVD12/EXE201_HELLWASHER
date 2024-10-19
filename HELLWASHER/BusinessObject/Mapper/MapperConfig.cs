using AutoMapper;
using BusinessObject.Model.Request.CreateRequest;
using BusinessObject.Model.Request.UpdateRequest.Entity;
using BusinessObject.Model.Request.UpdateRequest.Status;
using BusinessObject.Model.Response;
using BusinessObject.Model.Response.Login_SignUp;
using BusinessObject.ViewModels.OrderDTO;
using BusinessObject.ViewModels.ProductCheckoutDTO;
using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Mapper
{
    public class MapperConfig:Profile
    {
        public MapperConfig()
        {

            //Wash Service
            CreateMap<CreateWashServiceDTO, DataAccess.Entity.Service>().ReverseMap();
            CreateMap<ResponseWashServiceDTO, DataAccess.Entity.Service>().ReverseMap();
            //Category
            CreateMap<CreateCategoryDTO, Category>().ReverseMap();
            CreateMap<ResponseCategoryDTO, Category>().ReverseMap();
            //User
            CreateMap<User, CreateUserDTO>().ReverseMap();
            CreateMap<User, ResponseUserDTO>().ReverseMap();
            CreateMap<User, LoginRes>().ReverseMap();
            //Order 
            CreateMap<OrderDTO, Order>().ReverseMap();
            //Service Checkout
            CreateMap<CreateServiceCheckoutDTO, ServiceCheckout>().ReverseMap();
            CreateMap<ServiceCheckout, ResponseServiceCheckoutDTO>()
                .ForMember(a=>a.Name, d=>d.MapFrom(s=>s.Service))
                .ReverseMap();
            //Product Checkout
            CreateMap<ProductCheckoutDTO, ProductCheckout>().ReverseMap();
            //Product Item
            CreateMap<CreateProductDTO, Product>().ReverseMap();
            CreateMap<ResponseProductDTO, Product>().ReverseMap();
            CreateMap<UpdateProductDTO, Product>().ReverseMap();

        }
    }
}
