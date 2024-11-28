using Application.Dto.Moment;
using Application.Dto.Pagination;
using Domain.Filters;

namespace Application.Services.MomentService
{
    public interface IMomentService
    {
        PaginationResponse<MomentDto> GetAllMoments(FilterParams filterParams);
        Task<MomentDto?> GetMoment(long id);
        Task Create(CreateMomentDto createMomentDto, CancellationToken cancellationToken);
        Task<MomentDto> Put(long id, long userId, UpdateMomentDto updateMomentDto, CancellationToken cancellationToken);
        Task Delete(long id, long userId, CancellationToken cancellationToken);
    }
}
