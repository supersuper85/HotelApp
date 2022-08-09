namespace HotelApp.API.Extensions.ServiceCollection
{
    internal static class AddHttpClientExtension
    {
        internal static void AddHttpClientLayer(this IServiceCollection services)
        {
            Uri endPointA = new Uri("http://localhost:58919/");
            HttpClient httpClient = new HttpClient();
            //services.AddSingleton(httpClient);

            services.AddSingleton(httpClient);
        }
    }

}
