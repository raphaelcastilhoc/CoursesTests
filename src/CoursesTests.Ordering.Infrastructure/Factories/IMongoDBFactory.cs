using MongoDB.Driver;

namespace CoursesTests.Ordering.Infrastructure.Factories
{
    public interface IMongoDBFactory
    {
        IMongoDatabase CreateDatabase();
    }
}
