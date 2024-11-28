using Application.Dto.Pagination;
using Application.Dto.Review;
using Domain.Filters;

namespace Application.Services.ReviewService
{
    public interface IReviewService
    {
        PaginationResponse<ReviewDto> GetReviewsByRouteId(long routeId, FilterParams filterParams);
        PaginationResponse<ReviewDto> GetAllReviews(FilterParams filterParams);
        Task Create(long routeId, CreateReviewDto createReviewDto, CancellationToken cancellationToken);
        Task<ReviewDto> Put(long id, long userId, UpdateReviewDto updateReviewDto, CancellationToken cancellationToken);
        Task Delete(long id, long userId, CancellationToken cancellationToken);
        Task<ReviewDto?> GetReview(long id);
    }
}
