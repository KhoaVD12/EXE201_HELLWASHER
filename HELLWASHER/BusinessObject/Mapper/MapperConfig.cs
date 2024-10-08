using AutoMapper;
using BusinessObject.Model.Request.CreateRequest;
using BusinessObject.Model.Request.UpdateRequest.Status;
using BusinessObject.Model.Response;
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
            
            
            
            //Cart 
            CreateMap<CreateCartDTO, Cart>().ReverseMap();
            CreateMap< Cart,ResponseCartDTO > ()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.CartItems))
                .ReverseMap();
            CreateMap<ChangeCartStatusDTO, Cart>().ReverseMap();
            //Cart Item
            CreateMap<CreateCartItemDTO, CartItem>().ReverseMap();
            CreateMap<CartItem, ResponseCartItemDTO>()
                .ForMember(dest=>dest.ServiceName, opt=>opt.MapFrom(src=>src.Service))
                .ReverseMap();
        }
    }
}
