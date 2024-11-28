using Domain.Contracts.Entities;
using System.Linq.Expressions;

namespace Domain.Contracts.Repositories
{
    public interface IBaseRepository<T> where T : class, IEntity
    {
        Task<T?> GetById(long id);
        IQueryable<T> GetAll();
        IQueryable<T> Find(Expression<Func<T, bool>> expression);
        Task Add(T entity, CancellationToken cancellationToken);
        Task AddRange(IEnumerable<T> entities, CancellationToken cancellationToken);
        Task Remove(T entity, CancellationToken cancellationToken);
        Task<T> Update(T entity, CancellationToken cancellationToken);
    }
}
