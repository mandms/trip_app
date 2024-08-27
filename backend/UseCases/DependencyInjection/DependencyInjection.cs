﻿using UseCases.Services.UserService;
using Microsoft.Extensions.DependencyInjection;

namespace UseCases.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void AddBusinessLogicLayer(this IServiceCollection services)
        {
            services.InitServices();
        }
        private static void InitServices(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
        }
    }
}
