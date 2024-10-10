using AutoMapper;
using BusinessObject.Model.Request.CreateRequest;
using BusinessObject.Model.Request.UpdateRequest.Status;
using BusinessObject.Model.Response;
using BusinessObject.Model.Response.Login_SignUp;
using BusinessObject.ViewModels.OrderDTO;
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
            CreateMap<CreateWashServiceDTO, WashService>().ReverseMap();
            CreateMap<ResponseWashServiceDTO, WashService>().ReverseMap();
            //Category
            CreateMap<CreateCategoryDTO, Category>().ReverseMap();
            CreateMap<ResponseCategoryDTO, Category>().ReverseMap();
            //User
            CreateMap<User, CreateUserDTO>().ReverseMap();
            CreateMap<User, ResponseUserDTO>().ReverseMap();
            CreateMap<User, LoginRes>().ReverseMap();
            //Order 
            CreateMap<OrderDTO, Order>().ReverseMap();
            //Cart Item
            CreateMap<CreateCartItemDTO, ServiceCheckout>().ReverseMap();
            CreateMap<ResponseCartItemDTO, ServiceCheckout>().ReverseMap();
        }
    }
}
