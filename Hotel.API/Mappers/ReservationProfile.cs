using AutoMapper;
using HotelApp.API.Models;
using HotelApp.BLL.Dto;

namespace HotelApp.API.Mappers
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            CreateMap<ReservationModel, ReservationDto>().ReverseMap();
        }
    }
}
