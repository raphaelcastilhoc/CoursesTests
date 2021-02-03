using CoursesTests.Ordering.Application.UseCases;
using CoursesTests.Ordering.Domain.Aggregates.OrderAggregate;
using FizzWare.NBuilder;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CoursesTests.Ordering.Application.Tests.UseCases
{
    [TestClass]
    public class OrderUseCasesTests
    {
        private Mock<IOrderRepository> _orderRepository;
        private Mock<IHttpClientFactory> _httpClientFactory;

        private OrderUseCases _orderUseCases;


        [TestInitialize]
        public void Initialize()
        {
            _orderRepository = new Mock<IOrderRepository>();
            _httpClientFactory = new Mock<IHttpClientFactory>();

            _orderUseCases = new OrderUseCases(_orderRepository.Object,
                _httpClientFactory.Object);
        }

        [TestMethod]
        public async Task CreateOrderAsync_ShouldAdd()
        {
            //Arrange
            var orderItems = Builder<CreateOrderUseCase.OrderItem>.CreateListOfSize(3).Build();
            var orderUseCase = Builder<CreateOrderUseCase>
                .CreateNew()
                .With(x => x.OrderItems = orderItems)
                .Build();

            var expectedOrderItemsOutputDto = Builder<CreateOrderUseCaseResult.OrderItem>.CreateListOfSize(3).Build();
            var expectedResult = Builder<CreateOrderUseCaseResult>
                .CreateNew()
                .With(x => x.Id = string.Empty)
                .And(x => x.OrderItems = expectedOrderItemsOutputDto)
                .Build();

            //Act
            var result = await _orderUseCases.CreateOrderAsync(orderUseCase);

            //Assert
            result.Should().BeEquivalentTo(expectedResult, options => options.Excluding(order => order.Id));
            _orderRepository.Verify(x => x.AddAsync(It.IsAny<Order>()), Times.Once);
        }

        [TestMethod]
        public async Task CreateOrderItemAsync_ShouldAddOrderItemOnExistingOrder()
        {
            //Arrange
            var orderItemUseCase = Builder<CreateOrderItemUseCase>.CreateNew().Build();

            var order = new Order(1, new List<(int, int)>());
            _orderRepository.Setup(x => x.GetAsync(orderItemUseCase.OrderId)).ReturnsAsync(order);

            //Act
            await _orderUseCases.CreateOrderItemAsync(orderItemUseCase);

            //Assert
            _orderRepository.Verify(x => x.UpdateAsync(order), Times.Once);
        }

        [TestMethod]
        public async Task CreateOrderItemAsync_ShouldNotAddOrderItemIfOrderNotExists()
        {
            //Arrange
            var orderItemUseCase = Builder<CreateOrderItemUseCase>.CreateNew().Build();

            //Act
            await _orderUseCases.CreateOrderItemAsync(orderItemUseCase);

            //Assert
            _orderRepository.Verify(x => x.UpdateAsync(It.IsAny<Order>()), Times.Never);
        }
    }
}
