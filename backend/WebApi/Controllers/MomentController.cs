using Application.Dto.Moment;
using Application.Dto.Pagination;
using Application.Services.MomentService;
using Domain.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MomentController : ControllerBase
    {
        private readonly IMomentService _service;
        public MomentController(IMomentService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<PaginationResponse<MomentDto>> GetAll([FromQuery] FilterParams filterParams)
        {
            var pagedResponse = _service.GetAllMoments(filterParams);
            return Ok(pagedResponse);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MomentDto>> Get(long id)
        {
            var route = await _service.GetMoment(id);
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
        [HttpPost]
        public async Task<ActionResult<CreateMomentDto>> Post([FromBody] CreateMomentDto createMomentDto, CancellationToken cancellationToken)
        {
            long id = (long)HttpContext.Items[ClaimTypes.Sid]!;
            createMomentDto.UserId = id;
            await _service.Create(createMomentDto, cancellationToken);
            return Ok();
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<UpdateMomentDto>> Put(long id, UpdateMomentDto updateMomentDto, CancellationToken cancellationToken)
        {
            long userId = (long)HttpContext.Items[ClaimTypes.Sid]!;
            await _service.Put(id, userId, updateMomentDto, cancellationToken);
            return Ok();
        }
    }
}
