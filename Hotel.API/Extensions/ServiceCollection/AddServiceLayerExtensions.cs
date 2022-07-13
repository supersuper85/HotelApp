using HotelApp.BLL.Implementations;
using HotelApp.BLL.Interfaces;

namespace HotelApp.API.Extensions.ServiceCollection
{
    internal static class AddServiceLayerExtensions
    {
        internal static void AddServiceLayer(this IServiceCollection services)
        {
            services.AddTransient<IApartmentService, ApartmentService>();
            services.AddTransient<ICustomerService, CustomerService>();
        }
    }
}
