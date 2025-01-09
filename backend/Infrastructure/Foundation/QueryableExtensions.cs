using Domain.Entities;
using Domain.Filters;
using System.Linq.Expressions;

namespace Infrastructure.Foundation
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Filter<T>(this IQueryable<T> query, object filterParams)
        {
            switch (filterParams)
            {
                case FilterParamsWithTag tagParams when typeof(T) == typeof(Route):
                    query = ApplyTagFilter(query, tagParams);
                    break;

                case FilterParamsWithDate dateParams when typeof(T) == typeof(Moment):
                    query = ApplyDateFilterForMoment(query, dateParams);
                    break;

                case FilterParamsWithDate dateParams when typeof(T) == typeof(Review):
                    query = ApplydateFilterForReview(query, dateParams);
                    break;

                default:
                    throw new ArgumentException($"Unsupported filter parameters type or entity type: {typeof(T).Name}.", nameof(filterParams));
            }

            return query;
        }

        private static IQueryable<T> ApplydateFilterForReview<T>(IQueryable<T> query, FilterParamsWithDate dateParams)
        {
            if (dateParams.StartDate.HasValue)
            {
                dateParams.StartDate = DateTime.SpecifyKind(dateParams.StartDate.Value, DateTimeKind.Utc);
                query = query.Where(x => (x as Review)!.CreatedAt >= dateParams.StartDate.Value);
            }
            if (dateParams.EndDate.HasValue)
            {
                dateParams.EndDate = DateTime.SpecifyKind(dateParams.EndDate.Value, DateTimeKind.Utc);
                query = query.Where(x => (x as Review)!.CreatedAt <= dateParams.EndDate.Value);
            }

            return query;
        }

        private static IQueryable<T> ApplyDateFilterForMoment<T>(IQueryable<T> query, FilterParamsWithDate dateParams)
        {
            if (dateParams.StartDate.HasValue)
            {
                dateParams.StartDate = DateTime.SpecifyKind(dateParams.StartDate.Value, DateTimeKind.Utc);
                query = query.Where(x => (x as Moment)!.CreatedAt >= dateParams.StartDate.Value);
            }
            if (dateParams.EndDate.HasValue)
            {
                dateParams.EndDate = DateTime.SpecifyKind(dateParams.EndDate.Value, DateTimeKind.Utc);
                query = query.Where(x => (x as Moment)!.CreatedAt <= dateParams.EndDate.Value);
            }

            return query;
        }

        private static IQueryable<T> ApplyTagFilter<T>(IQueryable<T> query, FilterParamsWithTag tagParams)
        {
            if (!string.IsNullOrWhiteSpace(tagParams.Tag))
            {
                var tags = tagParams.Tag.Split(',').Select(tag => tag.Trim()).ToList();
                query = query.Where(x => (x as Route)!.Tags.Any(tag => tags.Contains(tag.Name)));
            }

            return query;
        }

        public static IQueryable<T> Sort<T>(this IQueryable<T> query, FilterParams filterParams)
        {
            if (string.IsNullOrWhiteSpace(filterParams.SortBy))
                return query;

            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, filterParams.SortBy);

            var orderByExp = Expression.Lambda<Func<T, object>>(Expression.Convert(property, typeof(object)), parameter);

            return (filterParams.Ordering.ToString() == "asc") ? query.OrderBy(orderByExp) : query.OrderByDescending(orderByExp);
        }

        public static IQueryable<T> Search<T>(this IQueryable<T> query, FilterParams filterParams, params string[] searchFields)
        {
            if (string.IsNullOrWhiteSpace(filterParams.SearchTerm) || searchFields == null || searchFields.Length == 0)
                return query;

            var parameter = Expression.Parameter(typeof(T), "x");
            Expression? combinedExpression = null;

            var searchLower = filterParams.SearchTerm.ToLower();

            foreach (var fieldName in searchFields)
            {
                var property = Expression.PropertyOrField(parameter, fieldName);
                if (property.Type != typeof(string))
                    continue;

                var toLowerMethod = typeof(string).GetMethod("ToLower", Type.EmptyTypes);
                var propertyToLower = Expression.Call(property, toLowerMethod!);

                var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                var searchExpression = Expression.Call(propertyToLower, containsMethod!, Expression.Constant(searchLower, typeof(string)));

                combinedExpression = combinedExpression == null
                    ? searchExpression
                    : Expression.OrElse(combinedExpression, searchExpression);
            }

            if (combinedExpression == null)
                return query;

            var lambda = Expression.Lambda<Func<T, bool>>(combinedExpression, parameter);
            return query.Where(lambda);
        }

    }
}
