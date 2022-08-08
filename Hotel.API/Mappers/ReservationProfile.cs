using AutoMapper;
using HotelApp.API.Models.ReservationModels;
using HotelApp.BLL.Dto;

namespace HotelApp.API.Mappers
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            CreateMap<ReservationModel, ReservationDto>().ReverseMap();
            CreateMap<ReservationDeleteModel, ReservationDto>().ReverseMap();

            CreateMap<ReservationCreateModel, ReservationDto>()
                .AfterMap((src, dest) =>
                {
                    dest.Customer.Id = src.Customer.Id;

                    dest.RegistrationDate = DateTime.UtcNow;
                    dest.ReleaseDate = GetReleaseDate(src.NumberOfDays);
                });

            CreateMap<ReservationEditModel, ReservationDto>()
                .AfterMap((src, dest) =>
                {
                    dest.Customer = new CustomerDto();
                });
        }

        public DateTime GetReleaseDate(int numberOfDays)
        {
            var initialDate = DateTime.UtcNow.AddDays(numberOfDays);
            int releaseHour = 12;
            int releaseMinute = 0;
            int releaseSecond = 0;
            DateTime releaseDate = new DateTime(initialDate.Year, initialDate.Month, initialDate.Day, releaseHour, releaseMinute, releaseSecond);
            return releaseDate;
        }
    }
}
