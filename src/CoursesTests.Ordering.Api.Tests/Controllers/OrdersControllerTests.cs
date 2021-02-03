using CoursesTests.Ordering.Api.Controllers;
using CoursesTests.Ordering.Application.UseCases;
using FizzWare.NBuilder;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace CoursesTests.Ordering.Api.Tests.Controllers
{
    [TestClass]
    public class OrdersControllerTests
    {
        private Mock<IOrderUseCases> _orderUseCases;

        private OrdersController _ordersController;

        [TestInitialize]
        public void Initialize()
        {
            _orderUseCases = new Mock<IOrderUseCases>();

            _ordersController = new OrdersController(_orderUseCases.Object);
        }

        [TestMethod]
        public async Task Post_ShouldAddOrder()
        {
            //Arrange
            var useCase = Builder<CreateOrderUseCase>.CreateNew().Build();
            var addedOrder = Builder<CreateOrderUseCaseResult>.CreateNew().Build();

            _orderUseCases.Setup(x => x.AddOrderAsync(useCase)).ReturnsAsync(addedOrder);

            var expectedResult = new CreatedResult("orders/", addedOrder.Id);

            //Act
            var result = await _ordersController.Post(useCase);

            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [TestMethod]
        public async Task CreateCheckout_ShouldCreateCheckout()
        {
            //Arrange
            var expectedResult = new OkResult();

            //Act
            var result = await _ordersController.CreateCheckout(It.IsAny<string>());

            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}
