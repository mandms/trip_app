using Domain.Contracts.Entities;
using Domain.Contracts.Repositories;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace WebApi.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class AuthorizeOwnerOrAdminAttribute : Attribute, IAsyncActionFilter
    {
        private readonly Type _entityType;

        public AuthorizeOwnerOrAdminAttribute(Type entityType)
        {
            if (!typeof(IEntity).IsAssignableFrom(entityType))
            {
                throw new ArgumentException($"Type {entityType.Name} must implement IEntity.");
            }

            _entityType = entityType;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var httpContext = context.HttpContext;

            if (httpContext.Request.Method == HttpMethods.Post && _entityType == typeof(Location))
            {
                await HandleLocationPostRequest(context);
                await next();
                return;
            }

            if (httpContext.Request.Method == HttpMethods.Post || httpContext.Request.Method == HttpMethods.Put || httpContext.Request.Method == HttpMethods.Delete)
            {
                await HandleEntityModification(context);
            }

            await next();
        }

        private async Task HandleLocationPostRequest(ActionExecutingContext context)
        {
            var userId = GetUserId(context.HttpContext);
            if (TryGetArgument(context, "routeId", out long routeId))
            {
                await ValidateRouteOwnership(context.HttpContext, routeId, userId);
            }
            else if (TryGetArgument(context, "id", out long locationId))
            {
                await HandleLocationModification(context.HttpContext, locationId, userId);
            }
            else
            {
                throw new BadHttpRequestException("Invalid resource ID");
            }
        }

        private async Task HandleEntityModification(ActionExecutingContext context)
        {
            var httpContext = context.HttpContext;
            var userId = GetUserId(httpContext);
            var resourceId = GetResourceIdFromArguments(context);

            switch (_entityType.Name)
            {
                case nameof(User):
                    HandleUserModification(httpContext, resourceId, userId);
                    break;
                case nameof(Location):
                    await HandleLocationModification(httpContext, resourceId, userId);
                    break;
                default:
                    await HandleGenericEntityModification(httpContext, resourceId, userId);
                    break;
            }
        }

        private long GetUserId(HttpContext context)
        {
            var userIdClaim = context.User.FindFirst(ClaimTypes.Sid);
            if (userIdClaim == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            return long.Parse(userIdClaim.Value);
        }

        private static bool TryGetArgument(ActionExecutingContext context, string argumentName, out long value)
        {
            value = 0;
            if (context.ActionArguments.TryGetValue(argumentName, out var argumentValue) && long.TryParse(argumentValue?.ToString(), out var parsedValue))
            {
                value = parsedValue;
                return true;
            }

            return false;
        }

        private long GetResourceIdFromArguments(ActionExecutingContext context)
        {
            if (TryGetArgument(context, "id", out var resourceId))
            {
                return resourceId;
            }

            throw new BadHttpRequestException("Invalid resource ID");
        }

        private void HandleUserModification(HttpContext context, long resourceId, long userId)
        {
            if (resourceId != userId && !IsAdmin(context))
            {
                throw new PermissionException();
            }
        }

        private async Task HandleLocationModification(HttpContext context, long locationId, long userId)
        {
            var locationRepository = GetService<IBaseRepository<Location>>(context);
            var routeRepository = GetService<IBaseRepository<Domain.Entities.Route>>(context);

            var location = await locationRepository.GetById(locationId);
            if (location == null)
            {
                throw new EntityNotFoundException(nameof(Location), locationId);
            }

            var route = await routeRepository.GetById(location.RouteId);
            if (route == null)
            {
                throw new EntityNotFoundException(nameof(Domain.Entities.Route), location.RouteId);
            }

            if (route.UserId != userId && !IsAdmin(context))
            {
                throw new PermissionException();
            }
        }

        private async Task ValidateRouteOwnership(HttpContext context, long routeId, long userId)
        {
            var routeRepository = GetService<IBaseRepository<Domain.Entities.Route>>(context);

            var route = await routeRepository.GetById(routeId);
            if (route == null)
            {
                throw new EntityNotFoundException(nameof(Domain.Entities.Route), routeId);
            }

            if (route.UserId != userId && !IsAdmin(context))
            {
                throw new PermissionException();
            }
        }

        private async Task HandleGenericEntityModification(HttpContext context, long resourceId, long userId)
        {
            var repositoryType = typeof(IBaseRepository<>).MakeGenericType(_entityType);
            var repository = GetService(context, repositoryType);

            var getByIdMethod = repository.GetType().GetMethod("GetById");
            var resourceTask = (Task)getByIdMethod!.Invoke(repository, new object[] { resourceId });
            await resourceTask;

            var resourceProperty = resourceTask.GetType().GetProperty("Result");
            var resource = resourceProperty!.GetValue(resourceTask);

            if (resource == null)
            {
                throw new EntityNotFoundException(_entityType.Name, resourceId);
            }

            if (resource is IUserOwnedEntity ownedEntity && ownedEntity.UserId != userId && !IsAdmin(context))
            {
                throw new PermissionException();
            }
        }

        private T GetService<T>(HttpContext context)
        {
            var service = context.RequestServices.GetService<T>();
            if (service == null)
            {
                throw new InvalidOperationException($"Service {typeof(T).Name} not registered.");
            }

            return service;
        }

        private object GetService(HttpContext context, Type serviceType)
        {
            var service = context.RequestServices.GetService(serviceType);
            if (service == null)
            {
                throw new InvalidOperationException($"Service {serviceType.Name} not registered.");
            }

            return service;
        }

        private bool IsAdmin(HttpContext context) => context.User.IsInRole(nameof(Roles.Admin));
    }
}
