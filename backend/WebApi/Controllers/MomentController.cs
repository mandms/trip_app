using Application.Dto.Image;
using Application.Dto.Location;
using Application.Dto.Moment;
using Application.Dto.Pagination;
using Application.Services.MomentService;
using Domain.Entities;
using Domain.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApi.Attributes;

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
        public ActionResult<PaginationResponse<MomentDto>> GetAll([FromQuery] FilterParamsWithDate filterParams)
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
        [AuthorizeOwnerOrAdmin(typeof(Moment))]
        public async Task<ActionResult> Delete(long id, CancellationToken cancellationToken)
        {
            await _service.Delete(id, cancellationToken);
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
        [AuthorizeOwnerOrAdmin(typeof(Moment))]
        public async Task<ActionResult<UpdateMomentDto>> Put(long id, UpdateMomentDto updateMomentDto, CancellationToken cancellationToken)
        {
            await _service.Put(id, updateMomentDto, cancellationToken);
            return Ok();
        }

        [Authorize]
        [HttpDelete("{id}/images")]
        [AuthorizeOwnerOrAdmin(typeof(Moment))]
        public async Task<ActionResult> DeleteImage(long id, [FromBody] List<long> imagesId, CancellationToken cancellationToken)
        {
            await _service.DeleteImages(id, imagesId, cancellationToken);
            return Ok();
        }

        [Authorize]
        [HttpPost("{id}/images")]
        [AuthorizeOwnerOrAdmin(typeof(Moment))]
        public async Task<ActionResult<CreateLocationDto>> AddImage(long id, [FromBody] CreateImagesDto createImagesDto, CancellationToken cancellationToken)
        {
            await _service.AddImages(id, createImagesDto, cancellationToken);
            return Ok();
        }
    }
}
