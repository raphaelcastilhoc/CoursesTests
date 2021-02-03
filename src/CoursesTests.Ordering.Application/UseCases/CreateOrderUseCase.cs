using System.Collections.Generic;

namespace CoursesTests.Ordering.Application.UseCases
{
    public class CreateOrderUseCase
    {
        public int CustomerId { get; set; }

        public IEnumerable<OrderItem> OrderItems { get; set; }

        public class OrderItem
        {
            public int ProductId { get; set; }

            public int Amount { get; set; }
        }
    }

    public class CreateOrderUseCaseResult
    {
        public string Id { get; set; }

        public int CustomerId { get; set; }

        public IEnumerable<OrderItem> OrderItems { get; set; }

        public class OrderItem
        {
            public int ProductId { get; set; }

            public int Amount { get; set; }
        }
    }
}
