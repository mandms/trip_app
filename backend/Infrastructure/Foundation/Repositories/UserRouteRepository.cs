using Domain.Contracts.Repositories;
using Domain.Entities;
using Domain.Filters;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Foundation.Repositories
{
    public class UserRouteRepository : BaseRepository<UserRoute>, IUserRouteRepository
    {
        public UserRouteRepository(TripAppDbContext context) : base(context)
        {
        }

        public IQueryable<UserRoute> GetAllRoutesByUserId(long userId, FilterParams filterParams)
        {
            var query = _context.Set<UserRoute>().
                Include(ur => ur.Route).
                ThenInclude(r => r.User).
                Include(ur => ur.Route).
                ThenInclude(r => r.Tags).
                Where(ur => ur.UserId == userId).
                Filter(filterParams).
                Sort(filterParams);

            return query.AsNoTracking();
        }

        public async Task<UserRoute?> GetUserRouteById(long userId, long routeId)
        {
            return await _context.Set<UserRoute>().FirstOrDefaultAsync(e => e.UserId == userId && e.RouteId == routeId);
        }
    }
}
