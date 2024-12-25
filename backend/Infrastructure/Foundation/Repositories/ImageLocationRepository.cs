using Domain.Contracts.Repositories;
using Domain.Entities;

namespace Infrastructure.Foundation.Repositories
{
    public class ImageLocationRepository : BaseRepository<ImageLocation>, IImageLocationRepository
    {
        public ImageLocationRepository(TripAppDbContext context) : base(context)
        {
        }
    }
}
