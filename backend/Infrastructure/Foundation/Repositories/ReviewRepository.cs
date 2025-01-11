using Domain.Contracts.Repositories;
using Domain.Entities;

namespace Infrastructure.Foundation.Repositories
{
    internal class ReviewRepository : BaseRepository<Review>, IReviewRepository
    {
        public ReviewRepository(TripAppDbContext context) : base(context)
        {
        }

        public double GetAverageRate(long routeId)
        {
            IQueryable<Review> review = _context.Set<Review>()
                .Where(review => review.RouteId == routeId);

            if (review.Any())
            {
                return 0;
            }

            return review.Join(
                    _context.Set<Route>(),
                    review => review.RouteId,
                    route => route.Id,
                    (review, route) =>
                    new {
                        review.Rate,
                    })
                .Average(r => r.Rate);
        }
    }
}
