using CoursesTests.Ordering.Application.UseCases;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace CoursesTests.Ordering.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderUseCases _orderUseCases;

        public OrdersController(IOrderUseCases orderUseCases)
        {
            _orderUseCases = orderUseCases;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> Post([FromBody] CreateOrderUseCase useCase)
        {
            var result = await _orderUseCases.CreateOrderAsync(useCase);
            return Created("orders/", result.Id);
        }

        [HttpPatch]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [Route("{id}/orderItem")]
        public async Task<IActionResult> CreateOrderItem(string id, [FromBody] CreateOrderItemInput input)
        {
            var useCase = new CreateOrderItemUseCase
            {
                OrderId = id,
                ProductId = input.ProductId,
                Amount = input.Amount
            };

            await _orderUseCases.CreateOrderItemAsync(useCase);
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [Route("{id}/checkout")]
        public async Task<IActionResult> CreateCheckout(string id)
        {
            var useCase = new CreateCheckoutUseCase { OrderId = id };

            await _orderUseCases.CreateCheckoutAsync(useCase);
            return Ok();
        }
    }
}
