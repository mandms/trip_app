using Domain.Repositories;
using Infrastructure.Foundation;
using Infrastructure.Foundation.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure.DependencyInjection;
public static class DependencyInjection
{
    public static void AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("TripAppDbContext");

        services.AddDbContext<TripAppDbContext>(options =>
        {
            options.UseNpgsql(connectionString, x => { x.UseNetTopologySuite(); x.MigrationsAssembly("Infrastructure"); }).LogTo(Console.WriteLine, LogLevel.Information);
        });
        services.InitRepositories();
    }

    private static void InitRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
    }
}
