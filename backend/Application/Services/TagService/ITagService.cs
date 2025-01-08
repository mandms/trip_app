using Application.Dto.Pagination;
using Application.Dto.Tag;
using Domain.Filters;

namespace Application.Services.TagService
{
    public interface ITagService
    {
        Task Create(long categoryId, CreateTagDto createTagDto, CancellationToken cancellationToken);
        Task Put(long id, UpdateTagDto updateTagDto, CancellationToken cancellationToken);
        Task Delete(long id, CancellationToken cancellationToken);
        Task<TagDto?> GetTag(long id);
        PaginationResponse<TagDto> GetAllTags(FilterParams filterParams);
    }
}
