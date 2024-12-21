using Application.Services.CategoryService;
using Application.Services.FileService;
using Application.Services.LocationService;
using Application.Services.MomentService;
using Application.Services.ReviewService;
using Application.Services.RouteService;
using Application.Services.TagService;
using Application.Services.UserRouteService;
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
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<IMomentService, MomentService>();
            services.AddTransient<IReviewService, ReviewService>();
            services.AddTransient<ITagService, TagService>();
            services.AddTransient<IUserRouteService, UserRouteService>();
            services.AddTransient<ICategoryService, CategoryService>();
        }
    }
}
