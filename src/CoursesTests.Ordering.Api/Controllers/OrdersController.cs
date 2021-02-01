﻿using CoursesTests.Ordering.Appication.UseCases;
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
            var result = await _orderUseCases.AddOrderAsync(useCase);
            return Created("orders/", result.Id);
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