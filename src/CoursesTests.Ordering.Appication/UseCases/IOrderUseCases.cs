using System.Threading.Tasks;

namespace CoursesTests.Ordering.Appication.UseCases
{
    public interface IOrderUseCases
    {
        Task<CreateOrderUseCaseResult> AddOrderAsync(CreateOrderUseCase useCases);

        Task CreateCheckoutAsync(CreateCheckoutUseCase useCase);
    }
}
