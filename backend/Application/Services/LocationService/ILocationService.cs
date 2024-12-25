﻿using Application.Dto.Image;
using Application.Dto.Location;

namespace Application.Services.LocationService
{
    public interface ILocationService
    {
        Task Create(CreateLocationDto createLocationDto, long routeId, CancellationToken cancellationToken);
        Task Put(long id, long userId, UpdateLocationDto updateLocationDto, CancellationToken cancellationToken);
        Task Delete(long id, long userId, CancellationToken cancellationToken);
        Task DeleteImages(long locationId, List<long> imageIds, CancellationToken cancellationToken);
        Task AddImages(long locationId, CreateImagesDto createImagesDto, CancellationToken cancellationToken);
    }
}
