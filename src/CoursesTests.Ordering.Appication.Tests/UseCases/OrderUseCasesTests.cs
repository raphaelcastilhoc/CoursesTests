using CoursesTests.Ordering.Appication.UseCases;
using CoursesTests.Ordering.Domain.Aggregates.OrderAggregate;
using FizzWare.NBuilder;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CoursesTests.Ordering.Appication.Tests.UseCases
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
        public async Task AddOrderAsync_ShouldAdd()
        {
            //Arrange
            var orderItemsInputDto = Builder<CreateOrderUseCase.OrderItem>.CreateListOfSize(3).Build();
            var orderInputDto = Builder<CreateOrderUseCase>
                .CreateNew()
                .With(x => x.OrderItems = orderItemsInputDto)
                .Build();

            var expectedOrderItemsOutputDto = Builder<CreateOrderUseCaseResult.OrderItem>.CreateListOfSize(3).Build();
            var expectedResult = Builder<CreateOrderUseCaseResult>
                .CreateNew()
                .With(x => x.Id = string.Empty)
                .And(x => x.OrderItems = expectedOrderItemsOutputDto)
                .Build();

            //Act
            var result = await _orderUseCases.AddOrderAsync(orderInputDto);

            //Assert
            result.Should().BeEquivalentTo(expectedResult);
            _orderRepository.Verify(x => x.AddAsync(It.IsAny<Order>()), Times.Once);
        }
    }
}
