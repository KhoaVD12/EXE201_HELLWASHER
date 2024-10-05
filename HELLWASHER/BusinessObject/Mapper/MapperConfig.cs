using AutoMapper;
using BusinessObject.Model.Request;
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
            //User
            CreateMap<CreateUserDTO, User>().ReverseMap();
            CreateMap<ResponseUserDTO, User>()
                .ForMember(dest => dest.Cart, opt => opt.MapFrom(src => src.Cart))
                .ReverseMap();
            //Wash Service
            CreateMap<CreateWashServiceDTO, WashService>().ReverseMap();
            CreateMap<ResponseWashServiceDTO, WashService>().ReverseMap();
            //Category
            CreateMap<CreateCategoryDTO, Category>().ReverseMap();
            CreateMap<ResponseCategoryDTO, Category>().ReverseMap();
            //Wash Service Type
            CreateMap<CreateWashServiceTypeDTO, WashServiceType>().ReverseMap();
            CreateMap<ResponseWashServiceTypeDTO,WashServiceType>().ReverseMap();
            //Wash Service Status
            CreateMap<CreateWashServiceStatusDTO, WashServiceStatus>().ReverseMap();
            CreateMap<ResponseWashServiceStatusDTO, WashServiceStatus>().ReverseMap();
            //Cloth Unit
            CreateMap<CreateClothUnitDTO, ClothUnit>().ReverseMap();
            CreateMap<ResponseClothUnitDTO, ClothUnit>().ReverseMap();
            //Cart 
            CreateMap<CreateCartDTO, Cart>().ReverseMap();
            CreateMap<ResponseCartDTO, Cart>().ReverseMap();

        }
    }
}
