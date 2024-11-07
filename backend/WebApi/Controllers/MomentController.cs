using Application.Dto.Moment;
using Application.Services.MomentService;
using Domain.Filters;
using Application.Dto.Pagination;
using Microsoft.AspNetCore.Mvc;

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

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id, CancellationToken cancellationToken)
        {
            await _service.Delete(id, cancellationToken);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<CreateMomentDto>> Post([FromBody] CreateMomentDto createMomentDto, CancellationToken cancellationToken)
        {
            await _service.Create(createMomentDto, cancellationToken);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UpdateMomentDto>> Put(long id, UpdateMomentDto updateMomentDto, CancellationToken cancellationToken)
        {
            await _service.Put(id, updateMomentDto, cancellationToken);
            return Ok();
        }

    }
}
