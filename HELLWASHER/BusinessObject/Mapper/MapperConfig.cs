﻿using AutoMapper;
using BusinessObject.Model.Request.CreateRequest;
using BusinessObject.Model.Request.UpdateRequest.Entity;
using BusinessObject.Model.Request.UpdateRequest.Status;
using BusinessObject.Model.Response;
using BusinessObject.Model.Response.Login_SignUp;
using BusinessObject.ViewModels.Authen;
using BusinessObject.ViewModels.Feedback;
using BusinessObject.ViewModels.Order;
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
            CreateMap<UpdateWashServiceDTO, DataAccess.Entity.Service>().ReverseMap();
            //Category
            CreateMap<CreateCategoryDTO, ProductCategory>().ReverseMap();
            CreateMap<ResponseCategoryDTO, ProductCategory>().ReverseMap();
            CreateMap<ServiceCategory, CreateCategoryDTO>().ReverseMap();
            CreateMap<ServiceCategory, ResponseCategoryDTO>().ReverseMap();
            //User
            CreateMap<User, CreateUserDTO>().ReverseMap();
            CreateMap<User, ResponseUserDTO>().ReverseMap();
            CreateMap<User, LoginRes>().ReverseMap();
            //Order 
            CreateMap<OrderDTO, Order>().ReverseMap();
            CreateMap<QuickOrderDTO, Order>().ReverseMap();
            CreateMap<AddOrderResponse, Order>().ReverseMap();
            CreateMap<OrderResponse, Order>().ReverseMap();
            CreateMap<Order, OrderResponse>().ReverseMap();
            //Service Checkout
            CreateMap<CreateServiceCheckoutDTO, ServiceCheckout>().ReverseMap();
            CreateMap<ServiceCheckout, ResponseServiceCheckoutDTO>()
                .ForMember(a=>a.Name, d=>d.MapFrom(s=>s.Service.Name))
                .ReverseMap();
            //Product Checkout
            CreateMap<ProductCheckoutDTO, ProductCheckout>().ReverseMap();
            CreateMap<ProductCheckout, ResponseProductCheckoutDTO>()
                .ForMember(a => a.Name, d => d.MapFrom(s => s.Product.Name))
                .ReverseMap();
            //Product Item
            CreateMap<CreateProductDTO, Product>().ReverseMap();
            CreateMap<ResponseProductDTO, Product>().ReverseMap();
            CreateMap<UpdateProductDTO, Product>().ReverseMap();

            //Feedback
            CreateMap<FeedbackDTO, Feedback>().ReverseMap();
            CreateMap<FeedbackRequest, Feedback>().ReverseMap();
            //CreateMap<Feedback, FeedbackDTO>().ReverseMap();

            //PayOs 
            CreateMap<Net.payOS.Types.PaymentLinkInformation, DataAccess.Entity.PaymentLinkInformation>().ReverseMap();
            CreateMap<Net.payOS.Types.Transaction, Transaction>().ReverseMap();
            //Authen
            CreateMap<RegisterRequest, User>().ReverseMap();
            CreateMap<LoginRequest, User>().ReverseMap();
        }
    }
}
