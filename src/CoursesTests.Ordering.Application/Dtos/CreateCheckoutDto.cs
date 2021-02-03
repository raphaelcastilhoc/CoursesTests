namespace CoursesTests.Ordering.Application.Dtos
{
    public class CreateCheckoutDto
    {
        public CreateCheckoutDto(string orderId)
        {
            OrderId = orderId;
        }

        public string OrderId { get; set; }
    }
}
