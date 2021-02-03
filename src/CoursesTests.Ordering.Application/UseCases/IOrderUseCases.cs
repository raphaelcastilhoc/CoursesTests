using System.Threading.Tasks;

namespace CoursesTests.Ordering.Application.UseCases
{
    public interface IOrderUseCases
    {
        Task<CreateOrderUseCaseResult> AddOrderAsync(CreateOrderUseCase useCases);

        Task CreateCheckoutAsync(CreateCheckoutUseCase useCase);
    }
}
