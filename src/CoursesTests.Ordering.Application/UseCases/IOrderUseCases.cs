using System.Threading.Tasks;

namespace CoursesTests.Ordering.Application.UseCases
{
    public interface IOrderUseCases
    {
        Task<CreateOrderUseCaseResult> CreateOrderAsync(CreateOrderUseCase useCases);

        Task CreateOrderItemAsync(CreateOrderItemUseCase useCase);

        Task CreateCheckoutAsync(CreateCheckoutUseCase useCase);
    }
}
