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

        public MomentService(
            IMomentRepository repository,
            IFileService fileService,
            IDbTransaction dbTransaction)
        {
            _repository = repository;
            _fileService = fileService;
            _dbTransaction = dbTransaction;
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

        public async Task Delete(long id, long userId, CancellationToken cancellationToken)
        {
            var moment = await _repository.GetMomentById(id);
            if (moment == null)
            {
                throw new MomentNotFoundException(id);
            }

            CheckUser(userId, moment);

            await _repository.Remove(moment, cancellationToken);
        }

        public PaginationResponse<MomentDto> GetAllMoments(FilterParams filterParams)
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
                throw new MomentNotFoundException(id);
            }

            return MomentMapper.MomentToMomentDto(moment);
        }

        public async Task<MomentDto> Put(long id, long userId, UpdateMomentDto updateMomentDto, CancellationToken cancellationToken)
        {
            Moment? foundMoment = await _repository.GetMomentById(id);
            if (foundMoment == null)
            {
                throw new RouteNotFoundException(id);
            }

            CheckUser(userId, foundMoment);

            MomentMapper.UpdateMoment(foundMoment, updateMomentDto);

            Moment moment = await _repository.Update(foundMoment, cancellationToken);
            return MomentMapper.MomentToMomentDto(moment);
        }

        private void CheckUser(long userId, Moment moment)
        {
            if (moment.UserId != userId)
            {
                throw new UnauthorizedAccessException("You are not authorized to edit this location.");
            }
        }
    }
}
