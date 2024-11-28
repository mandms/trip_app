using Application.Dto.Pagination;
using Application.Dto.Tag;
using Application.Mappers;
using Domain.Contracts.Repositories;
using Domain.Entities;
using Domain.Filters;

namespace Application.Services.TagService
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _repository;
        private readonly ICategoryRepository _categoryRepository;

        public TagService(
            ITagRepository repository,
            ICategoryRepository categoryRepository)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
        }
        public async Task Create(long categoryId, CreateTagDto createTagDto, CancellationToken cancellationToken)
        {
            Category? category = await _categoryRepository.GetById(categoryId);
            if (category == null)
            {
                throw new Exception("Category not found");
            }

            Tag tag = TagMapper.ToTag(createTagDto, categoryId);
            await _repository.Add(tag, cancellationToken);
        }

        public async Task Delete(long id, CancellationToken cancellationToken)
        {
            var tag = await _repository.GetById(id);
            if (tag == null)
            {
                throw new Exception("Tag not found");
            }

            await _repository.Remove(tag, cancellationToken);
        }

        public PaginationResponse<TagDto> GetAllTags(FilterParams filterParams)
        {
            var tags = _repository.GetAllTags(filterParams);

            var tagDtos = tags.Select(tag => TagMapper.TagToTagDto(tag));

            var pagedResponse = new PaginationResponse<TagDto>(tagDtos, filterParams.PageNumber, filterParams.PageSize);

            return pagedResponse;
        }

        public async Task<TagDto?> GetTag(long id)
        {
            var tag = await _repository.GetById(id);
            if (tag == null)
            {
                throw new Exception("Tag not found");
            }

            return TagMapper.TagToTagDto(tag);
        }

        public async Task<TagDto> Put(long id, UpdateTagDto updateTagDto, CancellationToken cancellationToken)
        {
            Tag? foundTag = await _repository.GetById(id);
            if (foundTag == null)
            {
                throw new Exception("Tag not found");
            }

            TagMapper.UpdateTag(updateTagDto, foundTag);

            Tag tag = await _repository.Update(foundTag, cancellationToken);
            return TagMapper.TagToTagDto(tag);
        }
    }
}
