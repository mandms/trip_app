using Domain.Entities;
using Domain.Filters;

namespace Domain.Contracts.Repositories
{
    public interface IMomentRepository : IBaseRepository<Moment>
    {
        IQueryable<Moment> GetAllMoments(FilterParams filterParams);
        Task<Moment?> GetMomentById(long id);
    }
}
