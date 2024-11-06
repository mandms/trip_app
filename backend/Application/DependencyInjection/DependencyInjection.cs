using Application.Services.LocationService;
using Application.Services.RouteService;
using Application.Services.UserService;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void AddBusinessLogicLayer(this IServiceCollection services)
        {
            services.InitServices();
        }
        private static void InitServices(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRouteService, RouteService>();
            services.AddTransient<ILocationService, LocationService>();
        }
    }
}
