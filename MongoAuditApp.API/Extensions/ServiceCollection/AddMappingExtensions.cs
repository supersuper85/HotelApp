using AutoMapper;
using api = MongoAuditApp.API.Mappers;
using bll = MongoAuditApp.BLL.Mappers;

namespace MongoAuditApp.API.Extensions.ServiceCollection
{
    internal static class AddMappingExtensions
    {
        internal static void AddMapping(this IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new api.MongoAuditProfile());

                cfg.AddProfile(new bll.MongoAuditProfile());

            });

            services.AddTransient(c => config.CreateMapper());
        }
    }
}
