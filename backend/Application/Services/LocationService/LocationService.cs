using Domain.Entities;
using Application.Mappers;
using Domain.Contracts.Repositories;
using Application.Dto.Location;
using Domain.Exceptions;

namespace Application.Services.LocationService
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _repository;
        private readonly IDbTransaction _dbTransaction;

        public LocationService(ILocationRepository repository, IDbTransaction dbTransaction) 
        {
            _repository = repository;
            _dbTransaction = dbTransaction;
        }

        public async Task Create(CreateLocationDto createLocationDto, long routeId, CancellationToken cancellationToken)
        {
            Location location = LocationMapper.ToLocation(createLocationDto, routeId);
            var createLocation = () => CreateLocation(routeId, location, cancellationToken);
            await _dbTransaction.Transaction(createLocation);
        }

        private async Task CreateLocation(long routeId, Location location, CancellationToken cancellationToken)
        {
            var maxOrder = _repository.GetMaxOrder(routeId);
            location.Order = maxOrder + 1;
            await _repository.Add(location, cancellationToken);
        }

        public async Task Put(long id, UpdateLocationDto updateLocationDto, CancellationToken cancellationToken)
        {
            var location = await _repository.GetById(id);
            if (location == null)
            {
                throw new LocationNotFoundException(id);
            }
            LocationMapper.UpdateLocationDtoLocation(location, updateLocationDto);
            await _repository.Update(location, cancellationToken);
        }

        public async Task Delete(long id, CancellationToken cancellationToken)
        {
            var location = await _repository.GetById(id);
            if (location == null)
            {
                throw new LocationNotFoundException(id);
            }

            await _repository.Remove(location, cancellationToken);
        }
    }
}
