using Domain.Contracts.Repositories;
using Domain.Entities;

namespace Infrastructure.Foundation.Repositories
{
    public class TagRepository : BaseRepository<Tag>, ITagRepository
    {
        public TagRepository(TripAppDbContext context) : base(context)
        {
        }
    }
}
