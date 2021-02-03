using CoursesTests.Ordering.Domain.SeedWork;
using System.Collections.Generic;
using System.Linq;

namespace CoursesTests.Ordering.Domain.Aggregates.OrderAggregate
{
    public class Order : Entity, IAggregateRoot
    {
        public Order(int customerId, IEnumerable<(int productId, int amount)> orderItems)
        {
            CustomerId = customerId;
            Items = new List<OrderItem>();

            foreach (var orderItem in orderItems)
            {
                AddItem(orderItem.productId, orderItem.amount);
            }
        }

        public int CustomerId { get; private set; }
            
        public ICollection<OrderItem> Items { get; private set; }

        public void AddItem(int productId, int amount)
        {
            var existingItem = Items.Where(item => item.ProductId == productId).FirstOrDefault();
            if(existingItem != null)
            {
                existingItem.AddAmount(amount);
            }
            else
            {
                var item = new OrderItem(productId, amount);
                Items.Add(item);
            }
        }
    }
}
