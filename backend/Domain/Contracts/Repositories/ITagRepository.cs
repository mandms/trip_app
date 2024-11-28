using Domain.Entities;
using Domain.Filters;

namespace Domain.Contracts.Repositories
{
    public interface ITagRepository : IBaseRepository<Tag>
    {
        IQueryable<Tag> GetAllTags(FilterParams filterParams);
        IQueryable<Tag> GetRangeTags(List<long> tagIds);
    }
}
