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

        public async Task<Route?> GetRouteById(long id)
        {
            var query = _context.Set<Route>().
                Join(_context.Set<User>(),
                r => r.UserId,
                u => u.Id,
                (r, u) => new Route
                {
                    Id = r.Id,
                    Description = r.Description,
                    Status = r.Status,
                    Duration = r.Duration,
                    Name = r.Name,
                    User = r.User,
                    Tags = r.Tags,
                });
            return await query.FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}
