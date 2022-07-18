﻿using AutoMapper;
using HotelApp.BLL.Dto;
using HotelApp.API.Models.HotelModels;

namespace HotelApp.API.Mappers.ModelMappers
{
    public class HotelProfile : Profile
    {
        public HotelProfile()
        {
            CreateMap<HotelModel, HotelDto>().ReverseMap();
        }
    }
}
