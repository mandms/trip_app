using Domain.Contracts.Repositories;
using Domain.Entities;

namespace Infrastructure.Foundation.Repositories
{
    internal class ImageMomentRepository : BaseRepository<ImageMoment>, IImageMomentRepository
    {
        public ImageMomentRepository(TripAppDbContext context) : base(context)
        {
        }
    }
}
