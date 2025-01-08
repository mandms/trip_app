using Application.Dto.Image;
using Application.Dto.Moment;
using Application.Dto.Pagination;
using Application.Mappers;
using Application.Services.FileService;
using Domain.Contracts.Repositories;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Filters;

namespace Application.Services.MomentService
{
    public class MomentService : IMomentService
    {
        private readonly IMomentRepository _repository;
        private readonly IFileService _fileService;
        private readonly IDbTransaction _dbTransaction;
        private readonly IImageMomentRepository _imageMomentRepository;

        public MomentService(
            IMomentRepository repository,
            IFileService fileService,
            IDbTransaction dbTransaction,
            IImageMomentRepository imageMomentRepository)
        {
            _repository = repository;
            _fileService = fileService;
            _dbTransaction = dbTransaction;
            _imageMomentRepository = imageMomentRepository;
        }

        public async Task Create(CreateMomentDto createMomentDto, CancellationToken cancellationToken)
        {
            var createMoment = () => CreateMoment(createMomentDto, cancellationToken);
            await _dbTransaction.Transaction(createMoment);
        }

        private async Task CreateMoment(CreateMomentDto createMomentDto, CancellationToken cancellationToken)
        {
            if ((createMomentDto.Images != null) && (createMomentDto.Images.Count() > 0))
            {
                var tasks = _fileService.SaveImages(createMomentDto.Images, cancellationToken);
                await Task.WhenAll(tasks);
            }

            Moment moment = MomentMapper.ToMoment(createMomentDto);
            await _repository.Add(moment, cancellationToken);
        }

        public async Task Delete(long id, CancellationToken cancellationToken)
        {
            var moment = await _repository.GetMomentById(id);
            if (moment == null)
            {
                throw new EntityNotFoundException("Moment", id);
            }

            await _repository.Remove(moment, cancellationToken);
        }

        public PaginationResponse<MomentDto> GetAllMoments(FilterParamsWithDate filterParams)
        {
            var moments = _repository.GetAllMoments(filterParams);

            var momentDtos = moments.Select(moment => MomentMapper.MomentToMomentDto(moment));

            var pagedResponse = new PaginationResponse<MomentDto>(momentDtos, filterParams.PageNumber, filterParams.PageSize);

            return pagedResponse;
        }

        public async Task<MomentDto?> GetMoment(long id)
        {
            var moment = await _repository.GetMomentById(id);
            if (moment == null)
            {
                throw new EntityNotFoundException("Moment", id);
            }

            return MomentMapper.MomentToMomentDto(moment);
        }

        public async Task<MomentDto> Put(long id, UpdateMomentDto updateMomentDto, CancellationToken cancellationToken)
        {
            Moment? foundMoment = await _repository.GetMomentById(id);
            if (foundMoment == null)
            {
                throw new EntityNotFoundException("Moment", id);
            }

            MomentMapper.UpdateMoment(foundMoment, updateMomentDto);

            Moment moment = await _repository.Update(foundMoment, cancellationToken);
            return MomentMapper.MomentToMomentDto(moment);
        }

        public async Task DeleteImages(long momentId, List<long> imageIds, CancellationToken cancellationToken)
        {
            var moment = await _repository.GetById(momentId);
            if (moment == null)
            {
                throw new EntityNotFoundException("Moment", momentId);
            }

            var deleteImages = () => DeleteImagesTransaction(imageIds, moment, cancellationToken);

            await _dbTransaction.Transaction(deleteImages);
        }

        private async Task DeleteImagesTransaction(List<long> imageIds, Moment moment, CancellationToken cancellationToken)
        {
            if (imageIds == null || !imageIds.Any())
            {
                throw new ArgumentException("No image IDs provided.");
            }

            foreach (var imageId in imageIds)
            {
                var imageToRemove = await _imageMomentRepository.GetById(imageId);
                if (imageToRemove == null)
                {
                    throw new EntityNotFoundException("Image", imageId);
                }

                _fileService.DeleteFile(imageToRemove.Image);

                moment.Images.Remove(imageToRemove);

                await _imageMomentRepository.Remove(imageToRemove, cancellationToken);
            }

            await _repository.Update(moment, cancellationToken);
        }

        public async Task AddImages(long momentId, List<CreateImageDto> createImagesDto, CancellationToken cancellationToken)
        {
            var moment = await _repository.GetById(momentId);
            if (moment == null)
            {
                throw new EntityNotFoundException("Moment", momentId);
            }

            var addImages = () => CreateImages(createImagesDto, moment, cancellationToken);

            await _dbTransaction.Transaction(addImages);
        }

        private async Task CreateImages(List<CreateImageDto> createImagesDto, Moment moment, CancellationToken cancellationToken)
        {
            if (createImagesDto != null && createImagesDto.Any())
            {
                var tasks = _fileService.SaveImages(createImagesDto, cancellationToken);
                await Task.WhenAll(tasks);

                var images = MomentMapper.GetImages(createImagesDto);
                foreach (var image in images)
                {
                    moment.Images.Add(image);
                }
            }
            await _repository.Update(moment, cancellationToken);
        }
    }
}
