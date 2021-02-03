using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CoursesTests.Ordering.Application.UseCases
{
    public class CreateOrderItemInput
    {
        public int ProductId { get; set; }

        public int Amount { get; set; }
    }

    public class CreateOrderItemUseCase
    {
        public string OrderId { get; set; }

        public int ProductId { get; set; }

        public int Amount { get; set; }
    }
}
