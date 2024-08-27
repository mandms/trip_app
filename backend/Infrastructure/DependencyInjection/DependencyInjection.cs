using Domain.Contracts.Repositories;
using Infrastructure.Foundation;
using Infrastructure.Foundation.Repositories;
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
    }

    private static void InitRepositories(this IServiceCollection services)
    { 
        services.AddScoped<IUserRepository, UserRepository>();
    }
}
