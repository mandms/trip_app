﻿using Application.Dto.Pagination;
using Application.Dto.Route;
using Application.Mappers;
using Application.Services.FileService;
using Domain.Contracts.Repositories;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Filters;

namespace Application.Services.RouteService
{
    public class RouteService : IRouteService
    {
        private readonly IRouteRepository _routeRepository;
        private readonly ITagRepository _tagRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IDbTransaction _dbTransaction;
        private readonly IUserRouteRepository _userRouteRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IFileService _fileService;

        public RouteService(
            IRouteRepository routeRepository,
            ITagRepository tagRepository,
            IReviewRepository reviewRepository,
            ILocationRepository locationRepository,
            IDbTransaction dbTransaction,
            IUserRouteRepository userRouteRepository,
            IFileService fileService
            )
        {
            _routeRepository = routeRepository;
            _tagRepository = tagRepository;
            _locationRepository = locationRepository;
            _dbTransaction = dbTransaction;
            _userRouteRepository = userRouteRepository;
            _reviewRepository = reviewRepository;
            _fileService = fileService;
        }

        public PaginationResponse<GetAllRoutesDto> GetAllRoutes(FilterParams filterParams)
        {
            var routes = _routeRepository.GetAllRoutes(filterParams).ToList();

            List<GetAllRoutesDto> routeDtos = new();

            routes.ForEach(route =>
            {
                double rate = _reviewRepository.GetAverageRate(route.Id);
                var getAllRouteDtos = RouteMapper.RouteToGetAllRoutesDto(route, rate);
                routeDtos.Add(getAllRouteDtos);
            });

            var pagedResponse = new PaginationResponse<GetAllRoutesDto>(routeDtos.AsQueryable(), filterParams.PageNumber, filterParams.PageSize);

            return pagedResponse;
        }

        public async Task<RouteDto?> GetRoute(long id)
        {
            var route = await _routeRepository.GetRouteById(id);
            if (route == null)
            {
                throw new EntityNotFoundException("Route", id);
            }

            return RouteMapper.RouteToRouteDto(route);
        }

        public async Task DeleteRoute(long id, long userId, CancellationToken cancellationToken)
        {
            var route = await _routeRepository.GetRouteById(id);
            if (route == null)
            {
                throw new EntityNotFoundException("Route", id);
            }

            CheckUser(userId, route);

            await _routeRepository.Remove(route, cancellationToken);
        }

        public async Task Create(CreateRouteDto createRouteDto, CancellationToken cancellationToken)
        {
            foreach (var location in createRouteDto.Locations)
            {
                if ((location.Images != null) && location.Images.Any())
                {
                    var tasks = _fileService.SaveImages(location.Images, cancellationToken);
                    await Task.WhenAll(tasks);
                }
            }
            var createRoute = () => CreateRoute(createRouteDto, cancellationToken);
            await _dbTransaction.Transaction(createRoute);
        }

        private List<Task<Tag>> CreateTagTasks(CreateRouteDto createRouteDto)
        {
            return createRouteDto.Tags.Select(async tagId =>
            {
                Tag? tag = await _tagRepository.GetById(tagId);

                if (tag == null)
                {
                    throw new EntityNotFoundException("Tag", tagId);
                }

                return tag;
            }).ToList();
        }

        private async Task CreateRoute(
            CreateRouteDto createRouteDto,
            CancellationToken cancellationToken
            )
        {
            List<Tag> tags = _tagRepository.GetRangeTags(createRouteDto.Tags).ToList();

            Route route = RouteMapper.CreateRouteDtoToRoute(createRouteDto, tags);

            await _routeRepository.Add(route, cancellationToken);

            UserRoute userRoute = RouteMapper.ToUserRoute(createRouteDto.UserId, route.Id);

            List<Location> locations = LocationMapper.ToLocations(createRouteDto, route.Id);

            await _userRouteRepository.Add(userRoute, cancellationToken);
            await _locationRepository.AddRange(locations, cancellationToken);
        }

        public async Task<RouteDto> UpdateRoute(long id, long userId, UpdateRouteDto updateRouteDto, CancellationToken cancellationToken)
        {
            Route? foundRoute = await _routeRepository.GetRouteById(id);
            if (foundRoute == null)
            {
                throw new EntityNotFoundException("Route", id);
            }
            CheckUser(userId, foundRoute);

            RouteMapper.UpdateRoute(foundRoute, updateRouteDto);

            Route route = await _routeRepository.Update(foundRoute, cancellationToken);
            return RouteMapper.RouteToRouteDto(route);
        }

        public async Task AddTag(long routeId, long tagId, long userId, CancellationToken cancellationToken)
        {
            var route = await _routeRepository.GetRouteById(routeId);
            if (route == null)
            {
                throw new EntityNotFoundException("Route", routeId);
            }

            CheckUser(userId, route);

            var tag = await _tagRepository.GetById(tagId);
            if (tag == null)
            {
                throw new EntityNotFoundException("Tag", tagId);
            }

            await _routeRepository.AddTag(route, tag, cancellationToken);
        }

        public async Task DeleteTag(long routeId, long tagId, long userId, CancellationToken cancellationToken)
        {
            var route = await _routeRepository.GetRouteById(routeId);
            if (route == null)
            {
                throw new EntityNotFoundException("Route", routeId);
            }

            CheckUser(userId, route);

            var tag = await _tagRepository.GetById(tagId);
            if (tag == null)
            {
                throw new EntityNotFoundException("Tag", tagId);
            }

            await _routeRepository.DeleteTag(route, tag, cancellationToken);
        }

        private void CheckUser(long userId, Route route)
        {
            if (route.UserId != userId)
            {
                throw new UnauthorizedAccessException("You are not authorized to edit this location.");
            }
        }
    }
}
