using Domain.Contracts.Repositories;
using Domain.Entities;
using Domain.Filters;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Foundation.Repositories
{
    public class RouteRepository : BaseRepository<Route>, IRouteRepository
    {
        public RouteRepository(TripAppDbContext context) : base(context)
        {
        }

        public async Task AddTag(Route route, Tag tag, CancellationToken cancellationToken)
        {
            route.Tags.Add(tag);
            await SaveAsync(cancellationToken);
        }

        public async Task DeleteTag(Route route, Tag tag, CancellationToken cancellationToken)
        {
            route.Tags.Remove(tag);
            await SaveAsync(cancellationToken);
        }

        public IQueryable<Route> GetAllRoutes(FilterParams filterParams)
        {
            var query = _context.Set<Route>().
                Include(r => r.User).
                Include(r => r.Tags).
                Where(r => r.Status == 1).
                Sort(filterParams);

            return query.AsNoTracking();
        }

        public async Task<Route?> GetRouteById(long id)
        {
            var query = _context.Set<Route>().
            Include(r => r.User).
            Include(r => r.Tags).
            Include(r => r.Locations).
            ThenInclude(l => l.Images);
            return await query.FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}
