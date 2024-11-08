using System.Security.Claims;

namespace WebApi.Middlewares
{
    public class RequestUserMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestUserMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if ((context.User.Identity != null) && context.User.Identity.IsAuthenticated)
            {
                context.Items.Add(ClaimTypes.Sid, Convert.ToInt64(
                                context.User.Claims.Where(c => c.Type == ClaimTypes.Sid)
                                .Select(c => c.Value)
                                .SingleOrDefault()
                                ));
            }
            await _next(context);
        }
    }
}
