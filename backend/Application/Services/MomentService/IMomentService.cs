using Application.Dto.Moment;
using Domain.Filters;
using Application.Dto.Pagination;

namespace Application.Services.MomentService
{
    public interface IMomentService
    {
        PaginationResponse<MomentDto> GetAllMoments(FilterParams filterParams);
        Task<MomentDto?> GetMoment(long id);
        Task Create(CreateMomentDto createMomentDto, CancellationToken cancellationToken);
        Task<MomentDto> Put(long id, UpdateMomentDto updateMomentDto, CancellationToken cancellationToken);
        Task Delete(long id, CancellationToken cancellationToken);
    }
}
