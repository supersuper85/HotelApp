using HotelApp.Data.Entities;
using HotelApp.Data.Entities.Context;
using HotelApp.Data.Implementations;
using HotelApp.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelApp.API.Extensions.ServiceCollection
{
    internal static class AddPersistenceLayerExtension
    {
        internal static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataBaseContext>(options =>
                options.UseSqlServer(configuration["ConnectionStrings:HotelConnection"]));

            //Custom interface for apartment
            services.AddTransient<IApartmentRepository<Apartment>, ApartmentRepository<Apartment>>();
            //Custom interface for reservation
            services.AddTransient<IReservationRepository<Reservation>, ReservationRepository<Reservation>>();

            services.AddTransient<IRepository<Apartment>, BaseEntityFrameworkRepository<Apartment>>();
            services.AddTransient<IRepository<Customer>, BaseEntityFrameworkRepository<Customer>>();
            services.AddTransient<IRepository<Reservation>, BaseEntityFrameworkRepository<Reservation>>();
            services.AddTransient<IRepository<Hotel>, BaseEntityFrameworkRepository<Hotel>>();

            
        }
    }
}
