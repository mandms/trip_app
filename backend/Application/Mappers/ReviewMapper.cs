using Application.Dto.Review;
using Domain.Entities;

namespace Application.Mappers
{
    public class ReviewMapper
    {
        public static ReviewDto ReviewToReviewDto(Review review)
        {
            return new ReviewDto
            {
                Id = review.Id,
                Text = review.Text,
                CreatedAt = review.CreatedAt.ToString("dd/MM/yyyy"),
                User = UserMapper.UserAuthor(review.User),
                Rate = review.Rate
            };
        }

        public static Review ToReview(long routeId, CreateReviewDto createReviewDto)
        {
            return new Review
            {
                Text = createReviewDto.Text,
                Rate = createReviewDto.Rate,
                UserId = createReviewDto.UserId,
                RouteId = routeId
            };
        }

        public static void UpdateReview(Review review, UpdateReviewDto updateReviewDto)
        {
            review.Text = updateReviewDto.Text;
            review.Rate = updateReviewDto.Rate;
        }
    }
}
