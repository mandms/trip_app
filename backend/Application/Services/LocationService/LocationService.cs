using Application.Dto.Image;
using Application.Dto.Location;
using Application.Mappers;
using Application.Services.FileService;
using Domain.Contracts.Repositories;
using Domain.Entities;
using Domain.Exceptions;
using static System.Net.Mime.MediaTypeNames;

namespace Application.Services.LocationService
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _repository;
        private readonly IDbTransaction _dbTransaction;
        private readonly IFileService _fileService;
        private readonly IRouteRepository _routeRepository;
        private readonly IImageLocationRepository _imageLocationRepository;

        public LocationService(
            ILocationRepository repository,
            IRouteRepository routeRepository,
            IDbTransaction dbTransaction,
            IFileService fileService,
            IImageLocationRepository imageLocationRepository
            )
        {
            _repository = repository;
            _dbTransaction = dbTransaction;
            _fileService = fileService;
            _routeRepository = routeRepository;
            _imageLocationRepository = imageLocationRepository;
        }

        public async Task Create(CreateLocationDto createLocationDto, long routeId, CancellationToken cancellationToken)
        {
            var route = await _routeRepository.GetById(routeId);
            if (route == null)
            {
                throw new EntityNotFoundException("Route", routeId);
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
                throw new EntityNotFoundException("Location", id);
            }

            await CheckUser(userId, location);

            if(updateLocationDto.Order > _repository.GetMaxOrder(location.RouteId))
            {
                throw new Exception("number exceeds maximum value");
            }

            LocationMapper.UpdateLocationDtoLocation(location, updateLocationDto);
            await _repository.Update(location, cancellationToken);
        }

        public async Task Delete(long id, long userId, CancellationToken cancellationToken)
        {
            var location = await _repository.GetById(id);
            if (location == null)
            {
                throw new EntityNotFoundException("Location", id);
            }

            await CheckUser(userId, location);

            await _repository.Remove(location, cancellationToken);
        }

        private async Task CheckUser(long userId, Location location)
        {
            var route = await _routeRepository.GetById(location.RouteId);
            if (route == null)
            {
                throw new EntityNotFoundException("Route", location.RouteId);
            }
            if (route.UserId != userId)
            {
                throw new UnauthorizedAccessException("You are not authorized to edit this location.");
            }
        }

        public async Task DeleteImages(long locationId, List<long> imageIds, CancellationToken cancellationToken)
        {
            var location = await _repository.GetById(locationId);
            if (location == null)
            {
                throw new EntityNotFoundException("Location", locationId);
            }

            var deleteImages = async () =>
            {
                if (imageIds == null || !imageIds.Any())
                {
                    throw new ArgumentException("No image IDs provided.");
                }

                foreach (var imageId in imageIds)
                {
                    var imageToRemove = await _imageLocationRepository.GetById(imageId);
                    if (imageToRemove == null)
                    {
                        throw new EntityNotFoundException("Image", imageId);
                    }

                    _fileService.DeleteFile(imageToRemove.Image);

                    location.Images.Remove(imageToRemove);

                    await _imageLocationRepository.Remove(imageToRemove, cancellationToken);
                }

                await _repository.Update(location, cancellationToken);
            };

            await _dbTransaction.Transaction(deleteImages);
        }



        public async Task AddImages(long locationId, CreateImagesDto createImagesDto, CancellationToken cancellationToken)
        {
            var location = await _repository.GetById(locationId);
            if (location == null)
            {
                throw new EntityNotFoundException("Location", locationId);
            }

            var addImages = () => CreateImages(createImagesDto, location, cancellationToken);

            await _dbTransaction.Transaction(addImages);
        }

        private async Task CreateImages(CreateImagesDto createImagesDto, Location location, CancellationToken cancellationToken)
        {
            if (createImagesDto.Images != null && createImagesDto.Images.Any())
            {
                var tasks = _fileService.SaveImages(createImagesDto.Images, cancellationToken);
                await Task.WhenAll(tasks);

                var images = LocationMapper.GetImages(createImagesDto.Images);
                foreach (var image in images)
                {
                    location.Images.Add(image);
                }
            }
            await _repository.Update(location, cancellationToken);
        }
    }
}
