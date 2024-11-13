using Domain.Entities;

namespace Domain.Contracts.Repositories
{
    public interface IReviewRepository : IBaseRepository<Review>
    {
        double GetAverageRate(long routeId);
    }
}
