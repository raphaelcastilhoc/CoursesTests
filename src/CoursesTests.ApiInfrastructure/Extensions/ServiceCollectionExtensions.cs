using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace CoursesTests.ApiInfrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services, string apiName)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = apiName, Version = "v1" });
            });

            return services;
        }

        public static IServiceCollection AddCustomHttpClient(this IServiceCollection services, string clientName, string baseUrl)
        {
            services
                .AddHttpClient(clientName, client =>
                {
                    client.BaseAddress = new Uri(baseUrl);
                });

            return services;
        }
    }
}
