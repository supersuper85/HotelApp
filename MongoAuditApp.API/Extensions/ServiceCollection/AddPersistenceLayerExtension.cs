using MongoAuditApp.Data.Interfaces;
using MongoAuditApp.Data.Implementations;
using MongoDB.Driver;

namespace MongoAuditApp.API.Extensions.ServiceCollection
{
    internal static class AddPersistenceLayerExtension
    {
        internal static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IMongoAuditRepository, MongoAuditRepository>();

            services.AddTransient<IMongoClient, MongoClient>(s =>
            {
                var uri = configuration["ConnectionString"];
                return new MongoClient(uri);
            });

            services.AddTransient(s =>
            {
                var client = s.GetService<IMongoClient>();
                var database = client.GetDatabase(configuration["DatabaseName"]);

                return database;
            });

        }
    }
}
