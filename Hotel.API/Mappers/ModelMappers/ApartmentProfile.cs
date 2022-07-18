using AutoMapper;
using HotelApp.BLL.Dto;
using HotelApp.API.Models.ApartmentModels;

namespace HotelApp.API.Mappers.ModelMappers
{
    public class ApartmentProfile : Profile
    {
        public ApartmentProfile()
        {
            CreateMap<ApartmentModel, ApartmentDto>().ReverseMap();
        }
    }
}
