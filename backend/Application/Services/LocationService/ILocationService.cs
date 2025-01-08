using Application.Dto.Image;
using Application.Dto.Location;

namespace Application.Services.LocationService
{
    public interface ILocationService
    {
        Task Create(CreateLocationDto createLocationDto, long routeId, CancellationToken cancellationToken);
        Task Put(long id, UpdateLocationDto updateLocationDto, CancellationToken cancellationToken);
        Task Delete(long id, CancellationToken cancellationToken);
        Task DeleteImages(long locationId, List<long> imageIds, CancellationToken cancellationToken);
        Task AddImages(long locationId, List<CreateImageDto> createImagesDto, CancellationToken cancellationToken);
    }
}
