namespace WebApi.Middlewares
{
    public static class RequestUserMiddlewareExtension
    {
        public static IApplicationBuilder UserRequestUserMiddleware(
        this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestUserMiddleware>();
        }
    }
}
