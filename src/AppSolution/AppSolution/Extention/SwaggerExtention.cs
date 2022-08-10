﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace AppSolution.Extention
{
    public static class SwaggerExtention
    {
        public static IServiceCollection AddSwaggerExtention(this IServiceCollection services, string title, string version)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = title, Version = version });
            });
            return services;
        }
        
    }
}