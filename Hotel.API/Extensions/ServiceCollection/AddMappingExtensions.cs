using AutoMapper;
using apiModelMappers = HotelApp.API.Mappers.ModelMappers;
using apiInputMappers = HotelApp.API.Mappers.InputModelMappers;
using bll = HotelApp.BLL.Mappers;

namespace HotelApp.API.Extensions.ServiceCollection
{
    internal static class AddMappingExtensions
    {
        internal static void AddMapping(this IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new apiModelMappers.CustomerProfile());
                cfg.AddProfile(new apiModelMappers.ApartmentProfile());
                cfg.AddProfile(new apiModelMappers.ReservationProfile());
                cfg.AddProfile(new apiModelMappers.HotelProfile());

                cfg.AddProfile(new apiInputMappers.InputCustomerProfile());
                cfg.AddProfile(new apiInputMappers.InputApartmentProfile());
                cfg.AddProfile(new apiInputMappers.InputReservationProfile());
                cfg.AddProfile(new apiInputMappers.InputHotelProfile());

                cfg.AddProfile(new bll.CustomerProfile());
                cfg.AddProfile(new bll.ApartmentProfile());
                cfg.AddProfile(new bll.ReservationProfile());
            });

            services.AddTransient(c => config.CreateMapper());
        }
    }
}
