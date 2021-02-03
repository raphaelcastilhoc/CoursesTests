using CoursesTests.Ordering.Application.Constants;
using CoursesTests.Ordering.Domain.Aggregates.OrderAggregate;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CoursesTests.Ordering.Application.UseCases
{
    public class OrderUseCases : IOrderUseCases
    {
        private readonly IOrderRepository _orderRepository;
        private readonly HttpClient _httpClient;

        public OrderUseCases(IOrderRepository orderRepository,
            IHttpClientFactory httpClientFactory)
        {
            _orderRepository = orderRepository;
            _httpClient = httpClientFactory.CreateClient(HttpClientName.Checkout);
        }

        public async Task<CreateOrderUseCaseResult> CreateOrderAsync(CreateOrderUseCase useCase)
        {
            var order = new Order(useCase.CustomerId,
                useCase.OrderItems.Select(item => (item.ProductId, item.Amount)));

            await _orderRepository.AddAsync(order);

            var useCaseResult = MapOrderToCreateOrderOutput(order);
            return useCaseResult;
        }

        private CreateOrderUseCaseResult MapOrderToCreateOrderOutput(Order order)
        {
            var useCaseResult = new CreateOrderUseCaseResult
            {
                Id = order.Id.ToString(),
                CustomerId = order.CustomerId,
                OrderItems = order.Items.Select(item =>
                new CreateOrderUseCaseResult.OrderItem
                {
                    ProductId = item.ProductId,
                    Amount = item.Amount
                })
            };

            return useCaseResult;
        }

        public async Task CreateOrderItemAsync(CreateOrderItemUseCase useCase)
        {
            var order = await _orderRepository.GetAsync(useCase.OrderId);

            if (order != null)
            {
                order.AddItem(useCase.ProductId, useCase.Amount);

                await _orderRepository.UpdateAsync(order);
            }
        }

        public async Task CreateCheckoutAsync(CreateCheckoutUseCase useCase)
        {
            var existingOrder = await _orderRepository.GetAsync(useCase.OrderId);
            if (existingOrder != null)
            {
                using (var content = new StringContent(JsonSerializer.Serialize(useCase), Encoding.UTF8, "application/json"))
                {
                    await _httpClient.PostAsync("checkouts", content);
                }
            }
        }
    }
}
