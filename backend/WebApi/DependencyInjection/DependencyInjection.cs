using Application.Dto.Pagination;
using Microsoft.OpenApi.Models;
using System.ComponentModel;

namespace WebApi.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void InitSwaggerGen(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Trip App API",
                    Description = "An ASP.NET Core Web API for trip app",
                    Contact = new OpenApiContact
                    {
                        Name = "Our repo",
                        Url = new Uri("https://github.com/mandms/trip_app")
                    }
                });
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
                 c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                 {
                     Name = "Authorization",
                     Type = SecuritySchemeType.ApiKey,
                     Scheme = "Bearer",
                     BearerFormat = "JWT",
                     In = ParameterLocation.Header,
                     Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                 });
                 c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                     {
                         new OpenApiSecurityScheme {
                             Reference = new OpenApiReference {
                                 Type = ReferenceType.SecurityScheme,
                                 Id = "Bearer"
                             }
                         },
                         new string[] {}
                     }
                });
             });
        }
    }
}
