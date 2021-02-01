using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace CoursesTests.Ordering.Infrastructure.Factories
{
    public class MongoDBFactory : IMongoDBFactory
    {
        private readonly IConfiguration _configuration;

        public MongoDBFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IMongoDatabase CreateDatabase()
        {
            var url = new MongoUrl(_configuration.GetValue<string>("ConnectionString"));
            var client = new MongoClient(url);
            var database = client.GetDatabase("Ordering");

            return database;
        }
    }
}
