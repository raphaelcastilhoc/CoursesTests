using CoursesTests.Ordering.Domain.Aggregates.OrderAggregate;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CoursesTests.Ordering.Domain.Tests.Aggregates.OrderAggregate
{
    [TestClass]
    public class OrderTests
    {
        [TestMethod]
        public void Constructor_ShouldCreateOrder()
        {
            //Arrange
            var customerId = 1;
            var orderItems = new List<(int productId, int amount)>
            {
                (1, 1)
            };

            var expectedCustomerId = 1;
            var expectedOrderItems = new List<OrderItem>
            {
                new OrderItem(1, 1)
            };

            //Act
            var result = new Order(customerId, orderItems);

            //Assert
            result.CustomerId.Should().Be(expectedCustomerId);
            result.Items.Should().BeEquivalentTo(expectedOrderItems, options => options.Excluding(orderItem => orderItem.Id));
        }

        [TestMethod]
        public void AddItem_ShouldIncrementAmountIfItemAlreadyExists()
        {
            //Arrange
            var orderItems = new List<(int productId, int amount)>
            {
                (1, 1)
            };

            var order = new Order(customerId: 1, orderItems);

            var expectedOrderItems = new List<OrderItem>
            {
                new OrderItem(1, 2)
            };

            //Act
            order.AddItem(productId: 1, amount: 1);

            //Assert
            order.Items.Should().BeEquivalentTo(expectedOrderItems, options => options.Excluding(orderItem => orderItem.Id));
        }

        [TestMethod]
        public void AddItem_ShouldAddItemIfNotExists()
        {
            //Arrange
            var customerId = 1;
            var orderItems = new List<(int productId, int amount)>
            {
                (1, 1)
            };

            var order = new Order(customerId, orderItems);

            var expectedOrderItems = new List<OrderItem>
            {
                new OrderItem(1, 1),
                new OrderItem(2, 2)
            };

            //Act
            order.AddItem(productId: 2, amount: 2);

            //Assert
            order.Items.Should().BeEquivalentTo(expectedOrderItems, options => options.Excluding(orderItem => orderItem.Id));
        }
    }
}
