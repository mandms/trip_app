using Domain.Entities;
using Domain.Filters;
using System.Linq.Expressions;
using System.Reflection;

namespace Infrastructure.Foundation
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Filter<T>(this IQueryable<T> query, FilterParams filterParams)
        {
            if (string.IsNullOrWhiteSpace(filterParams.TagFilter))
                return query;

            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, "Tags");

            var tagParameter = Expression.Parameter(typeof(Tag), "tag");
            var tagNameProperty = Expression.Property(tagParameter, "Name");

            var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });

            if (containsMethod == null)
            {
                throw new InvalidOperationException("The 'Contains' method was not found on the string type.");
            }

            var filterExpression = Expression.Call(tagNameProperty, containsMethod, Expression.Constant(filterParams.TagFilter));

            var asQueryableMethod = typeof(Queryable)
            .GetMethods()
            .First(m => m.Name == "AsQueryable" && m.GetParameters().Length == 1)
            .MakeGenericMethod(typeof(Tag));
            var asQueryableExpression = Expression.Call(asQueryableMethod, property);

            var anyMethod = typeof(Queryable).GetMethods()
                .First(m => m.Name == "Any" && m.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(Tag));

            var anyExpression = Expression.Call(
                anyMethod,
                asQueryableExpression,
                Expression.Lambda<Func<Tag, bool>>(filterExpression, tagParameter)
            );

            var lambda = Expression.Lambda<Func<T, bool>>(anyExpression, parameter);
            return query.Where(lambda);
        }


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
