using AutoMapper;
using HotelApp.API.InputModels;
using HotelApp.BLL.Dto;

namespace HotelApp.API.Mappers.InputModelMappers
{
    public class InputReservationProfile : Profile
    {
        public InputReservationProfile()
        {
            CreateMap<ReservationInputModel, ReservationDto>()
                .AfterMap((src, dest) =>
                {
                    dest.Customer.HotelId = src.HotelId;
                    dest.Customer.ApartmentId = src.ApartmentId;
                    dest.Customer.ApartmentId = src.ApartmentId;

                    dest.RegistrationDate = DateTime.UtcNow;
                    dest.ReleaseDate = GetReleaseDate(src.NumberOfDays);
                });
        }

        public DateTime GetReleaseDate(int NumberOfDays)
        {
            var InitialDate = DateTime.Now.AddDays(NumberOfDays);
            int ReleaseHour = 12;
            DateTime ReleaseDate = (new DateTime(InitialDate.Year, InitialDate.Month, InitialDate.Day, ReleaseHour, InitialDate.Minute, InitialDate.Second)).ToUniversalTime();
            return ReleaseDate;
        }
    }
}
