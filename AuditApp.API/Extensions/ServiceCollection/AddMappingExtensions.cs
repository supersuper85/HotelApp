using AutoMapper;
using api = AuditApp.API.Mappers;
using bll = AuditApp.BLL.Mappers;

namespace AuditApp.API.Extensions.ServiceCollection
{
    internal static class AddMappingExtensions
    {
        internal static void AddMapping(this IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new api.AuditProfile());

                cfg.AddProfile(new bll.AuditProfile());

            });

            services.AddTransient(c => config.CreateMapper());
        }
    }
}
