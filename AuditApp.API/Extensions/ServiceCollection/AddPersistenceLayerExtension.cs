using AuditApp.Data.Entities;
using AuditApp.Data.Entities.Context;
using AuditApp.Data.Implementations;
using AuditApp.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuditApp.API.Extensions.ServiceCollection
{
    internal static class AddPersistenceLayerExtension
    {
        internal static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataBaseContext>(options =>
                options.UseSqlServer(configuration["ConnectionStrings:HotelConnection"]));


            services.AddTransient<IAuditRepository, AuditRepository>();
            
        }
    }
}
