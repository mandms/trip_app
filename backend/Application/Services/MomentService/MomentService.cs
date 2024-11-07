using Application.Dto.Moment;
using Application.Mappers;
using Domain.Contracts.Repositories;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Filters;
using Application.Dto.Pagination;

namespace Application.Services.MomentService
{
    public class MomentService : IMomentService
    {
        private readonly IMomentRepository _repository;

        public MomentService(IMomentRepository repository)
        {
            _repository = repository;
        }

        public async Task Create(CreateMomentDto createMomentDto, CancellationToken cancellationToken)
        {
            Moment moment = MomentMapper.ToMoment(createMomentDto);
            await _repository.Add(moment, cancellationToken);
        }

        public async Task Delete(long id, CancellationToken cancellationToken)
        {
            var moment = await _repository.GetMomentById(id);
            if (moment == null)
            {
                throw new MomentNotFoundException(id);
            }

            await _repository.Remove(moment, cancellationToken);
        }

        public PaginationResponse<MomentDto> GetAllMoments(FilterParams filterParams)
        {
            var moments = _repository.GetAllMoments(filterParams);

            var momentDtos = moments.Select(moment => MomentMapper.MomentToMomentDto(moment));

            var pagedResponse = new PaginationResponse<MomentDto>( momentDtos, filterParams.PageNumber, filterParams.PageSize );

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

        public async Task<MomentDto> Put(long id, UpdateMomentDto updateMomentDto, CancellationToken cancellationToken)
        {
            Moment? foundMoment = await _repository.GetMomentById(id);
            if (foundMoment == null)
            {
                throw new RouteNotFoundException(id);
            }

            MomentMapper.UpdateMoment(foundMoment, updateMomentDto);

            Moment moment = await _repository.Update(foundMoment, cancellationToken);
            return MomentMapper.MomentToMomentDto(moment);
        }
    }
}
