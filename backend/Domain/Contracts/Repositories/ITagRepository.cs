using Domain.Entities;
using Domain.Filters;

namespace Domain.Contracts.Repositories
{
    public interface ITagRepository : IBaseRepository<Tag>
    {
        IQueryable<Tag> GetAllTags(FilterParams filterParams);
        public Task<Tag?> GetTagById(long id);
        IQueryable<Tag> GetRangeTags(List<long> tagIds);
    }
}
