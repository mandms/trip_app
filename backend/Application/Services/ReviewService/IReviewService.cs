using Application.Dto.Pagination;
using Application.Dto.Review;
using Domain.Filters;

namespace Application.Services.ReviewService
{
    public interface IReviewService
    {
        PaginationResponse<ReviewDto> GetReviewsByRouteId(long routeId, FilterParamsWithDate filterParams);
        PaginationResponse<ReviewDto> GetAllReviews(FilterParamsWithDate filterParams);
        Task Create(long routeId, CreateReviewDto createReviewDto, CancellationToken cancellationToken);
        Task<ReviewDto> Put(long id, UpdateReviewDto updateReviewDto, CancellationToken cancellationToken);
        Task Delete(long id, CancellationToken cancellationToken);
        Task<ReviewDto?> GetReview(long id);
    }
}
