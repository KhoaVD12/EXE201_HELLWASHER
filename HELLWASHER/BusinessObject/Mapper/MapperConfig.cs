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
            CreateMap<ResponseUserDTO, User>().ReverseMap();
            //Wash Service
            CreateMap<CreateWashServiceDTO, WashService>().ReverseMap();
            CreateMap<ResponseWashServiceDTO, WashService>().ReverseMap();
            //Category
            //Wash Service Type
            CreateMap<CreateWashServiceTypeDTO, WashServiceType>().ReverseMap();
            CreateMap<ResponseWashServiceTypeDTO,WashServiceType>().ReverseMap();

        }
    }
}
