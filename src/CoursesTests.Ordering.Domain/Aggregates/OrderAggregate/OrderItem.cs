using CoursesTests.Ordering.Domain.SeedWork;
using MongoDB.Bson;

namespace CoursesTests.Ordering.Domain.Aggregates.OrderAggregate
{
    public class OrderItem : Entity
    {
        public OrderItem(int productId, int amount)
        {
            ProductId = productId;
            Amount = amount;
        }

        public int ProductId { get; private set; }

        public int Amount { get; private set; }

        public void AddAmount(int amount)
        {
            Amount += amount;
        }
    }
}
