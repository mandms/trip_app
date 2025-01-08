using Application.Dto.Pagination;
using Application.Dto.Review;
using Application.Mappers;
using Domain.Contracts.Repositories;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Filters;

namespace Application.Services.ReviewService
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _repository;
        private readonly IRouteRepository _routeRepository;

        public ReviewService(
            IReviewRepository repository,
            IRouteRepository routeRepository)
        {
            _repository = repository;
            _routeRepository = routeRepository;
        }

        public async Task Create(long routeId, CreateReviewDto createReviewDto, CancellationToken cancellationToken)
        {
            Route? route = await _routeRepository.GetRouteById(routeId);
            if (route == null)
            {
                throw new EntityNotFoundException("Route", routeId);
            }

            Review review = ReviewMapper.ToReview(routeId, createReviewDto);
            await _repository.Add(review, cancellationToken);
        }

        public async Task Delete(long id, CancellationToken cancellationToken)
        {
            var review = await _repository.GetReviewById(id);
            if (review == null)
            {
                throw new EntityNotFoundException("Review", id);
            }

            await _repository.Remove(review, cancellationToken);
        }

        public PaginationResponse<ReviewDto> GetReviewsByRouteId(long routeId, FilterParamsWithDate filterParams)
        {
            var reviews = _repository.GetAllByRouteId(routeId, filterParams);

            var reviewDtos = reviews.Select(review => ReviewMapper.ReviewToReviewDto(review));

            var pagedResponse = new PaginationResponse<ReviewDto>(reviewDtos, filterParams.PageNumber, filterParams.PageSize);

            return pagedResponse;
        }

        public PaginationResponse<ReviewDto> GetAllReviews(FilterParamsWithDate filterParams)
        {
            var reviews = _repository.GetAllReviews(filterParams);

            var reviewDtos = reviews.Select(review => ReviewMapper.ReviewToReviewDto(review));

            var pagedResponse = new PaginationResponse<ReviewDto>(reviewDtos, filterParams.PageNumber, filterParams.PageSize);

            return pagedResponse;
        }

        public async Task<ReviewDto> Put(long id, UpdateReviewDto updateReviewDto, CancellationToken cancellationToken)
        {
            Review? foundReview = await _repository.GetReviewById(id);
            if (foundReview == null)
            {
                throw new EntityNotFoundException("Review", id);
            }

            ReviewMapper.UpdateReview(foundReview, updateReviewDto);

            Review review = await _repository.Update(foundReview, cancellationToken);
            return ReviewMapper.ReviewToReviewDto(review);
        }

        public async Task<ReviewDto?> GetReview(long id)
        {
            var review = await _repository.GetReviewById(id);
            if (review == null)
            {
                throw new EntityNotFoundException("Review", id);
            }

            return ReviewMapper.ReviewToReviewDto(review);
        }
    }
}
