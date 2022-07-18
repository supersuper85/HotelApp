using AutoMapper;
using HotelApp.API.Models.ReservationModels;
using HotelApp.BLL.Dto;

namespace HotelApp.API.Mappers.ModelMappers
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            CreateMap<ReservationModel, ReservationDto>().ReverseMap();
        }
    }
}
