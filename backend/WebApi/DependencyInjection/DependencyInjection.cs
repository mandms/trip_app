using Application.Dto.Pagination;
using System.ComponentModel;

namespace WebApi.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void InitSwaggerGen(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
             {
                 c.CustomSchemaIds(type =>
                 {
                     var displayNameAttribute = type.GetCustomAttributes(false)
                                                     .OfType<DisplayNameAttribute>()
                                                     .FirstOrDefault();

                     if (displayNameAttribute != null)
                     {
                         return displayNameAttribute.DisplayName;
                     }

                     if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(PaginationResponse<>))
                     {
                         var genericArgument = type.GetGenericArguments()[0];
                         return $"PaginationResponse-{genericArgument.Name}";
                     }

                     return type.Name;
                 });
             });
        }
    }
}
