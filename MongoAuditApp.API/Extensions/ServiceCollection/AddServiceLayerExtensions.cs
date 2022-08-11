using MongoAuditApp.BLL.Implementations;
using MongoAuditApp.BLL.Interfaces;

namespace MongoAuditApp.API.Extensions.ServiceCollection
{
    internal static class AddServiceLayerExtensions
    {
        internal static void AddServiceLayer(this IServiceCollection services)
        {
            services.AddTransient<IMongoAuditService, MongoAuditService>();
        }
    }
}
