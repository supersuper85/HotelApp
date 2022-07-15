using AutoMapper;
using HotelApp.API.InputModels;
using HotelApp.BLL.Dto;

namespace HotelApp.API.Mappers.InputModelMappers
{
    public class InputHotelProfile : Profile
    {
        public InputHotelProfile()
        {
            CreateMap<HotelInputModel, HotelDto>().ReverseMap();
        }
    }
}
