using Application.Dto.Location;
using Application.Mappers;
using Application.Services.FileService;
using Domain.Contracts.Repositories;
using Domain.Entities;
using Domain.Exceptions;

namespace Application.Services.LocationService
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _repository;
        private readonly IDbTransaction _dbTransaction;
        private readonly IFileService _fileService;
        private readonly IRouteRepository _routeRepository;

        public LocationService(
            ILocationRepository repository,
            IRouteRepository routeRepository,
            IDbTransaction dbTransaction,
            IFileService fileService
            )
        {
            _repository = repository;
            _dbTransaction = dbTransaction;
            _fileService = fileService;
            _routeRepository = routeRepository;
        }

        public async Task Create(CreateLocationDto createLocationDto, long routeId, CancellationToken cancellationToken)
        {
            var route = await _routeRepository.GetById(routeId);
            if (route == null)
            {
                throw new RouteNotFoundException(routeId);
            }
            var createLocation = () => CreateLocation(routeId, createLocationDto, cancellationToken);
            await _dbTransaction.Transaction(createLocation);
        }

        private async Task CreateLocation(long routeId, CreateLocationDto createLocationDto, CancellationToken cancellationToken)
        {
            if ((createLocationDto.Images != null) && (createLocationDto.Images.Count() > 0))
            {
                var tasks = _fileService.SaveImages(createLocationDto.Images, cancellationToken);
                await Task.WhenAll(tasks);
            }
            Location location = LocationMapper.ToLocation(createLocationDto, routeId);
            var maxOrder = _repository.GetMaxOrder(routeId);
            location.Order = maxOrder + 1;
            await _repository.Add(location, cancellationToken);
        }

        public async Task Put(long id, long userId, UpdateLocationDto updateLocationDto, CancellationToken cancellationToken)
        {
            var location = await _repository.GetById(id);
            if (location == null)
            {
                throw new LocationNotFoundException(id);
            }

            await CheckUser(userId, location);

            LocationMapper.UpdateLocationDtoLocation(location, updateLocationDto);
            await _repository.Update(location, cancellationToken);
        }

        public async Task Delete(long id, long userId, CancellationToken cancellationToken)
        {
            var location = await _repository.GetById(id);
            if (location == null)
            {
                throw new LocationNotFoundException(id);
            }

            await CheckUser(userId, location);

            await _repository.Remove(location, cancellationToken);
        }

        private async Task CheckUser(long userId, Location location)
        {
            var route = await _routeRepository.GetById(location.RouteId);
            if (route == null)
            {
                throw new RouteNotFoundException(location.RouteId);
            }
            if (route.UserId != userId)
            {
                throw new UnauthorizedAccessException("You are not authorized to edit this location.");
            }
        }
    }
}
