using AutoMapper;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Ordering.Api.Controllers;
using Ordering.Api.Models;
using Ordering.Core.Entities;
using Ordering.Core.Orders.Commands.CancelOrder;
using Ordering.Core.Orders.Commands.CreateOrder;
using Ordering.Core.Orders.Queries.GetOrdersByCustomerId;
using Ordering.Core.Orders.Queries.GetOrdersByDate;
using Ordering.UnitTests.Extensions;
using Xunit;

namespace Ordering.UnitTests.Controllers
{
    public class OrderControllerTest
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IMapper> _mapper;
        private readonly int _id;
        public OrderControllerTest()
        {
            _mediatorMock = new Mock<IMediator>();
            _mapper= new Mock<IMapper>();
            _id = 5;
        }

        [Fact]
        public async Task Cancel_order_with_Id_success()
        {
            //Arrange
            _mediatorMock.Setup(x => x.Send(It.IsAny<CancelOrderCommand>(), default))
                .Returns(Task.FromResult(true));

            //Act
            var orderController = new OrderController(_mediatorMock.Object,_mapper.Object);
            var actionResult = await orderController.CancelOrderAsync(_id) as OkResult;

            //Assert
            Assert.Equal(actionResult.StatusCode, (int)System.Net.HttpStatusCode.OK);

        }

        [Fact]
        public async Task Cancel_order_bad_request()
        {
            //Arrange
            _mediatorMock.Setup(x => x.Send(It.IsAny<CancelOrderCommand>(), default))
                .Returns(Task.FromResult(true));

            //Act
            var orderController = new OrderController(_mediatorMock.Object,_mapper.Object);
            var actionResult = await orderController.CancelOrderAsync(0) as BadRequestResult;

            //Assert
            Assert.Equal(actionResult.StatusCode, (int)System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_order_success()
        {
            //Arrange
            _mediatorMock.Setup(x => x.Send(It.IsAny<CreateOrderCommand>(), default))
                .Returns(Task.FromResult(0));

            //Act
            var orderController = new OrderController(_mediatorMock.Object,_mapper.Object);
            var actionResult = await orderController.CreateOrderAsync(new CreateOrderModel
            {
                CustomerId = 456,
                CustomerName = "CustomerName",
                OrderItems = new List<OrderItemModel>()
            }
            );

            //Assert
            Assert.Equal((actionResult.Result as OkObjectResult).StatusCode, (int)System.Net.HttpStatusCode.OK);

        }

        [Fact]
        public async void GetByDate_ShouldReturnOrders()
        {
            _mediatorMock.Setup(x => x.Send(It.IsAny<GetOrdersByDateQuery>(), default))
                .ReturnsAsync(new List<Order>()
                );

            var orderController = new OrderController(_mediatorMock.Object, _mapper.Object);
            var actionResult = await orderController.OrdersByDateAsync(DateTime.Now.AddDays(-1),DateTime.Now );

            actionResult.Should().NotBeNull();
            actionResult.Result.Should().BeOfType<OkObjectResult>();
            var order = actionResult.GetObjectResult();
            order.Should().BeOfType<List<Order>>();
        }

        [Fact]
        public async void GetByCustomer_ShouldReturnOrders()
        {
            _mediatorMock.Setup(x => x.Send(It.IsAny<GetOrdersByCustomerIdQuery>(), default))
                .ReturnsAsync(new List<Order>()
                );

            var orderController = new OrderController(_mediatorMock.Object, _mapper.Object);
            var actionResult = await orderController.OrdersByCustomerAsync(_id);

            actionResult.Should().NotBeNull();
            actionResult.Result.Should().BeOfType<OkObjectResult>();
            var order = actionResult.GetObjectResult();
            order.Should().BeOfType<List<Order>>();
        }

    }
}