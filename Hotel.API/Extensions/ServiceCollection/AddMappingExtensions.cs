using AutoMapper;
using api = HotelApp.API.Mappers;
using bll = HotelApp.BLL.Mappers;

namespace HotelApp.API.Extensions.ServiceCollection
{
    internal static class AddMappingExtensions
    {
        internal static void AddMapping(this IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new api.CustomerProfile());
                cfg.AddProfile(new api.ApartmentProfile());
                cfg.AddProfile(new api.ReservationProfile());
                cfg.AddProfile(new api.HotelProfile());

                cfg.AddProfile(new bll.CustomerProfile());
                cfg.AddProfile(new bll.ApartmentProfile());
                cfg.AddProfile(new bll.ReservationProfile());
            });

            services.AddTransient(c => config.CreateMapper());
        }
    }
}
