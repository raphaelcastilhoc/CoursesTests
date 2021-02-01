using CoursesTests.Ordering.Domain.Aggregates.OrderAggregate;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;
using System.Linq;

namespace CoursesTests.Ordering.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMongoCollection<Order> _orders;

        public OrderRepository(IMongoDatabase database)
        {
            _orders = database.GetCollection<Order>("orders");
        }

        public async Task<Order> GetAsync(string id)
        {
            var order = (await _orders.FindAsync(x => x.Id == ObjectId.Parse(id))).FirstOrDefault();
            return order;
        }

        public async Task AddAsync(Order order)
        {
            await _orders.InsertOneAsync(order);
        }

        public async Task UpdateAsync(Order order)
        {
            await _orders.ReplaceOneAsync(x => x.Id == order.Id, order);
        }
    }
}
