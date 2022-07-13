using AutoMapper;
using HotelApp.BLL.Dto;
using HotelApp.Data.Entities;

namespace HotelApp.BLL.Mappers
{
    public class ApartmentProfile : Profile
    {
        public ApartmentProfile()
        {
            CreateMap<ApartmentDto, Apartment>().ReverseMap();
        }
    }
}
