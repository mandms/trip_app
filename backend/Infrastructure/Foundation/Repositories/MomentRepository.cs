using Domain.Contracts.Repositories;
using Domain.Entities;
using Domain.Filters;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Foundation.Repositories
{
    public class MomentRepository : BaseRepository<Moment>, IMomentRepository
    {
        public MomentRepository(TripAppDbContext context) : base(context)
        {
        }
        public IQueryable<Moment> GetAllMoments(FilterParamsWithDate filterParams)
        {
            var query = _context.Set<Moment>().
                Include(m => m.User).
                Include(m => m.Images).
                Search(filterParams, "Description").
                Filter(filterParams).
                Sort(filterParams);

            return query.AsNoTracking();
        }

        public async Task<Moment?> GetMomentById(long id)
        {
            var query = _context.Set<Moment>().
            Include(m => m.User).
            Include(m => m.Images);
            return await query.FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
