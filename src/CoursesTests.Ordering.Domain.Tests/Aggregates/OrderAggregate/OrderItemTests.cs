using CoursesTests.Ordering.Domain.Aggregates.OrderAggregate;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoursesTests.Ordering.Domain.Tests.Aggregates.OrderAggregate
{
    [TestClass]
    public class OrderItemTests
    {
        [TestMethod]
        public void Constructor_ShouldCreateOrderItem()
        {
            //Arrange
            var productId = 1;
            var amount = 1;

            var expectedProductId = 1;
            var expectedAmount = 1;

            //Act
            var result = new OrderItem(productId, amount);

            //Assert
            result.ProductId.Should().Be(expectedProductId);
            result.Amount.Should().Be(expectedAmount);
        }

        [TestMethod]
        public void AddAmount_ShouldAddAmount()
        {
            //Arrange
            var orderItem = new OrderItem(productId: 1, amount: 1);

            var expectedAmount = 2;

            //Act
            orderItem.AddAmount(1);

            //Assert
            orderItem.Amount.Should().Be(expectedAmount);
        }
    }
}
