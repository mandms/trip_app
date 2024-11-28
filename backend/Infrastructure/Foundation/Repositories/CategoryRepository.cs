using Domain.Contracts.Repositories;
using Domain.Entities;

namespace Infrastructure.Foundation.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(TripAppDbContext context) : base(context)
        {
        }
    }
}
