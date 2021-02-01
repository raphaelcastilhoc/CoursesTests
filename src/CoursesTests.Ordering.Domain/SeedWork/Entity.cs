using MongoDB.Bson;

namespace CoursesTests.Ordering.Domain.SeedWork
{
    public class Entity
    {
        public Entity()
        {
            Id = ObjectId.GenerateNewId();
        }

        public Entity(ObjectId id)
        {
            Id = id;
        }

        public ObjectId Id { get; private set; }
    }
}
