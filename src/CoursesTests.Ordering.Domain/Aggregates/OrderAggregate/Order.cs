using CoursesTests.Ordering.Domain.SeedWork;
using System.Collections.Generic;
using System.Linq;

namespace CoursesTests.Ordering.Domain.Aggregates.OrderAggregate
{
    public class Order : Entity, IAggregateRoot
    {
        private readonly List<OrderItem> _orderItems;

        public Order(int customerId, IEnumerable<(int productId, int amount)> orderItems)
        {
            CustomerId = customerId;

            _orderItems = new List<OrderItem>();
            foreach (var item in orderItems)
            {
                AddItem(item.productId, item.amount);
            }
        }

        public int CustomerId { get; private set; }
            
        public IReadOnlyCollection<OrderItem> OrderItems { get => _orderItems; private set { } }

        public void AddItem(int productId, int amount)
        {
            var existingItem = OrderItems.Where(item => item.ProductId == productId).FirstOrDefault();
            if(existingItem != null)
            {
                existingItem.AddAmount(amount);
            }
            else
            {
                var item = new OrderItem(productId, amount);
                _orderItems.Add(item);
            }
        }
    }
}
