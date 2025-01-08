using Domain.Contracts.Repositories;
using Domain.Entities;
using Domain.Filters;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Foundation.Repositories
{
    public class TagRepository : BaseRepository<Tag>, ITagRepository
    {
        public TagRepository(TripAppDbContext context) : base(context)
        {
        }

        public async Task<Tag?> GetTagById(long id)
        {
            var query = _context.Set<Tag>().
                Include(t => t.Category);

            return await query.FirstOrDefaultAsync(t => t.Id == id);
        }

        public IQueryable<Tag> GetAllTags(FilterParams filterParams)
        {
            var query = _context.Set<Tag>().
                Include(t => t.Category).
                Search(filterParams, "Name").
                Sort(filterParams);

            return query.AsNoTracking();
        }

        public async Task<List<Tag>> GetRangeTags(List<long> tagIds)
        {
            var tags = await _context.Set<Tag>()
                .Where(t => tagIds.Contains(t.Id))
                .AsNoTracking() 
                .ToListAsync();

            return tags;
        }

    }
}
