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
            services.AddTransient<IApartmentRepository, ApartmentRepository>();
            //Custom interface for customer
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            //Custom interface for reservation
            services.AddTransient<IReservationRepository, ReservationRepository>();

            services.AddTransient<IRepository<Hotel>, BaseEntityFrameworkRepository<Hotel>>();
        }
    }
}
