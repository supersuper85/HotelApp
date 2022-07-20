using AutoMapper;
using HotelApp.BLL.Dto;
using HotelApp.API.Models.HotelModels;

namespace HotelApp.API.Mappers
{
    public class HotelProfile : Profile
    {
        public HotelProfile()
        {
            CreateMap<HotelModel, HotelDto>().ReverseMap();
            CreateMap<HotelPostModel, HotelDto>().ReverseMap();
        }
    }
}
