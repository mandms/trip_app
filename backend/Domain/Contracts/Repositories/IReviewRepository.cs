﻿using Domain.Entities;
using Domain.Filters;

namespace Domain.Contracts.Repositories
{
    public interface IReviewRepository : IBaseRepository<Review>
    {
        double GetAverageRate(long routeId);
        IQueryable<Review> GetAllByRouteId(long routeId, FilterParamsWithDate filterParams);
        IQueryable<Review> GetAllReviews(FilterParamsWithDate filterParams);
        Task<Review?> GetReviewById(long id);
    }
}
