using Domain.Filters;
using System.Linq.Expressions;

namespace Infrastructure.Foundation
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Sort<T>(this IQueryable<T> query, FilterParams filterParams)
        {
            if (string.IsNullOrWhiteSpace(filterParams.SortBy))
                return query;

            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, filterParams.SortBy);
            if (property == null)
                throw new ArgumentException($"Property '{filterParams.SortBy}' not found on type '{typeof(T).Name}'");

            var propertyAccess = Expression.Property(parameter, filterParams.SortBy);
            var orderByExp = Expression.Lambda<Func<T, object>>(Expression.Convert(propertyAccess, typeof(object)), parameter);

            return filterParams.IsAscending ? query.OrderBy(orderByExp) : query.OrderByDescending(orderByExp);
        }
    }
}
