using Application.Dto.Image;
using Application.Dto.Moment;
using Application.Dto.Pagination;
using Domain.Filters;

namespace Application.Services.MomentService
{
    public interface IMomentService
    {
        PaginationResponse<MomentDto> GetAllMoments(FilterParamsWithDate filterParams);
        Task<MomentDto?> GetMoment(long id);
        Task Create(CreateMomentDto createMomentDto, CancellationToken cancellationToken);
        Task<MomentDto> Put(long id, UpdateMomentDto updateMomentDto, CancellationToken cancellationToken);
        Task Delete(long id, CancellationToken cancellationToken);
        Task DeleteImages(long momentId, List<long> imageIds, CancellationToken cancellationToken);
        Task AddImages(long momentId, List<CreateImageDto> createImagesDto, CancellationToken cancellationToken);
    }
}
