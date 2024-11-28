using Domain.Contracts.Repositories;
using Domain.Entities;
using Domain.Filters;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Foundation.Repositories
{
    internal class ReviewRepository : BaseRepository<Review>, IReviewRepository
    {
        public ReviewRepository(TripAppDbContext context) : base(context)
        {
        }

        public IQueryable<Review> GetAllReviews(FilterParams filterParams)
        {
            var query = _context.Set<Review>().
                Include(review => review.User).
                Sort(filterParams);

            return query.AsNoTracking();
        }

        public IQueryable<Review> GetAllByRouteId(long routeId)
        {
            var query = _context.Set<Review>().
                Where(review => review.RouteId == routeId).
                Include(review => review.User);

            return query.AsNoTracking();
        }

        public double GetAverageRate(long routeId)
        {
            IQueryable<Review> review = _context.Set<Review>()
                .Where(review => review.RouteId == routeId);

            if (review.Count() == 0)
            {
                return 0;
            }

            return review.Join(
                    _context.Set<Route>(),
                    review => review.RouteId,
                    route => route.Id,
                    (review, route) =>
                    new
                    {
                        review.Rate,
                    })
                .Average(r => r.Rate);
        }

        public async Task<Review?> GetReviewById(long id)
        {
            var query = _context.Set<Review>().
            Include(r => r.User);
            return await query.FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}
