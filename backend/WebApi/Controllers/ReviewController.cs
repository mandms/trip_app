using Application.Dto.Pagination;
using Application.Dto.Review;
using Application.Services.ReviewService;
using Domain.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _service;
        public ReviewController(IReviewService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<PaginationResponse<ReviewDto>> GetAll([FromQuery] FilterParamsWithDate filterParams)
        {
            var pagedResponse = _service.GetAllReviews(filterParams);
            return Ok(pagedResponse);
        }

        [HttpGet("route/{routeId}")]
        public ActionResult<PaginationResponse<ReviewDto>> GetAllByRouteId(long routeId, [FromQuery] FilterParamsWithDate filterParams)
        {
            var pagedResponse = _service.GetReviewsByRouteId(routeId, filterParams);
            return Ok(pagedResponse);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewDto>> Get(long id)
        {
            var route = await _service.GetReview(id);
            return new ObjectResult(route);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id, CancellationToken cancellationToken)
        {
            long userId = (long)HttpContext.Items[ClaimTypes.Sid]!;
            await _service.Delete(id, userId, cancellationToken);
            return Ok();
        }

        [Authorize]
        [HttpPost("route/{routeId}")]
        public async Task<ActionResult<CreateReviewDto>> Post(long routeId, [FromBody] CreateReviewDto createReviewDto, CancellationToken cancellationToken)
        {
            long userId = (long)HttpContext.Items[ClaimTypes.Sid]!;
            createReviewDto.UserId = userId;
            await _service.Create(routeId, createReviewDto, cancellationToken);
            return Ok();
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<UpdateReviewDto>> Put(long id, UpdateReviewDto updateReviewDto, CancellationToken cancellationToken)
        {
            long userId = (long)HttpContext.Items[ClaimTypes.Sid]!;
            await _service.Put(id, userId, updateReviewDto, cancellationToken);
            return Ok();
        }
    }
}
