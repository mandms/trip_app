using Domain.Contracts.Entities;
using Domain.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Domain.Interfaces;
using System.Threading;

namespace Infrastructure.Foundation.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IEntity
    {
        protected readonly TripAppDbContext _context;
        public BaseRepository(TripAppDbContext context)
        {
            _context = context;
        }

        public async Task Add(T entity, CancellationToken cancellationToken)
        {
            await _context.Set<T>().AddAsync(entity);
            await SaveAsync(cancellationToken);
        }

        public async Task AddRange(IEnumerable<T> entities, CancellationToken cancellationToken)
        {
            await _context.Set<T>().AddRangeAsync(entities);
            await SaveAsync(cancellationToken);
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().AsNoTracking().Where(expression);
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsNoTracking().Where(u => u.Id == 1);
        }

        public async Task<T?> GetById(long id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task Remove(T entity, CancellationToken cancellationToken)
        {
            _context.Set<T>().Remove(entity);
            await SaveAsync(cancellationToken);
        }

        public async Task Update(T entity, CancellationToken cancellationToken)
        {
            await SaveAsync(cancellationToken);
        }

        protected async Task SaveAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}