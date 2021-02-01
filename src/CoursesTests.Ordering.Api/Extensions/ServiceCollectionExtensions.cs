using CoursesTests.Ordering.Appication.UseCases;
using CoursesTests.Ordering.Domain.Aggregates.OrderAggregate;
using CoursesTests.Ordering.Infrastructure.Factories;
using CoursesTests.Ordering.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace CoursesTests.Ordering.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddSingleton<IMongoDatabase>(x =>
            {
                return new MongoDBFactory(configuration).CreateDatabase();
            });

            return services;
        }

        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddScoped<IOrderUseCases, OrderUseCases>();

            return services;
        }
    }
}
