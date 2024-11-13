using Domain.Contracts.Repositories;
using Domain.Contracts.Utils;
using Infrastructure.Foundation;
using Infrastructure.Foundation.Repositories;
using Infrastructure.Providers;
using Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjection;
public static class DependencyInjection
{
    public static void AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("TripAppDbContext");

        services.AddDbContext<TripAppDbContext>(options =>
        {
            options.UseNpgsql(connectionString, x => { x.UseNetTopologySuite(); x.MigrationsAssembly("Infrastructure"); });
        });
        services.InitRepositories();
        services.InitUtils();
    }

    private static void InitRepositories(this IServiceCollection services)
    { 
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRouteRepository, RouteRepository>();
        services.AddScoped<ITagRepository, TagRepository>();
        services.AddScoped<ILocationRepository, LocationRepository>();
        services.AddScoped<IUserRouteRepository, UserRouteRepository>();
        services.AddScoped<IMomentRepository, MomentRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();
        services.AddTransient<IDbTransaction, DbTransaction>();
    }

    private static void InitUtils(this IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IJwtProvider, JwtProvider>();
    }
}
