using System.Threading.Tasks;

namespace CoursesTests.Ordering.Domain.Aggregates.OrderAggregate
{
    public interface IOrderRepository
    {
        Task<Order> GetAsync(string id);

        Task AddAsync(Order order);

        Task UpdateAsync(Order order);
    }
}
