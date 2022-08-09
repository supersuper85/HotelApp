using AuditApp.BLL.Implementations;
using AuditApp.BLL.Interfaces;

namespace AuditApp.API.Extensions.ServiceCollection
{
    internal static class AddServiceLayerExtensions
    {
        internal static void AddServiceLayer(this IServiceCollection services)
        {
            services.AddTransient<IAuditService, AuditService>();
        }
    }
}
