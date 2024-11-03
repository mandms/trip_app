using Domain.Contracts.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Foundation.Repositories
{
    public class RouteRepository : BaseRepository<Route>, IRouteRepository
    {
        public RouteRepository(TripAppDbContext context) : base(context)
        {
        }

        public IQueryable<Route> GetAllRoutes()
        {
            var query = _context.Set<Route>().
                Include(r => r.User).
                Include(r => r.Tags).
                Where(r => r.Status == 1);
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
