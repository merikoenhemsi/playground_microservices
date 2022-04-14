﻿using System.Net;
using AutoMapper;
using Customer.API.Controllers;
using Customer.API.Models;
using Customer.API.Repositories;
using Customer.Tests.Extensions;
using EventBus.Messages.Events;
using FluentAssertions;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;


namespace Customer.Tests.Controllers;

public class CustomerControllerTest
{
    private static readonly List<API.Entities.Customer> Customers
        = new()
        {
            new API.Entities.Customer
            {
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                FirstName = "meri",
                LastName = "kh",
                Email = "a@gmail.com",
                Address = "istanbul",
                Gender = "female"
            },
            new API.Entities.Customer
            {
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                FirstName = "FirstName",
                LastName = "LastName",
                Email = "Email",
                Address = "Address",
                Gender = "Gender"
            }
        };

    private static (CustomerController, Mock<ICustomerRepository>, Mock<ILogger<CustomerController>>, Mock<IMapper>, Mock<IPublishEndpoint>) Factory()
    {
        var logger = new Mock<ILogger<CustomerController>>();
        var repository = new Mock<ICustomerRepository>();
        var mapper = new Mock<IMapper>();
        var publishEndPoint = new Mock<IPublishEndpoint>();

        var controller = new CustomerController(repository.Object, logger.Object, mapper.Object, publishEndPoint.Object);
        return (controller, repository, logger, mapper, publishEndPoint);
    }

    [Fact]
    public async void Get_ShouldReturnCustomers()
    {
        var (controller, repository, _, _, _) = Factory();
        repository.Setup(rp => rp.GetCustomersAsync()).ReturnsAsync(Customers);
        var actionResult = await controller.CustomersAsync();
       
        actionResult.Should().NotBeNull();
        actionResult.Result.Should().BeOfType<OkObjectResult>();
        var result = actionResult.Result as OkObjectResult;
        var customers = actionResult.GetObjectResult();
        customers.Should().BeOfType<List<API.Entities.Customer>>();
        customers.Count.Should().Be(2);
    }

    [Fact]
    public async void Get_ShouldReturnCustomer()
    {
        var (controller, repository, _, _, _) = Factory();
        repository.Setup(rp => rp.GetCustomerAsync(It.IsAny<int>()))!.ReturnsAsync(Customers.FirstOrDefault());
        var actionResult = await controller.CustomerByIdAsync(5);

        actionResult.Should().NotBeNull();
        actionResult.Result.Should().BeOfType<OkObjectResult>();
        var customer = actionResult.GetObjectResult();
        customer.Should().BeOfType<API.Entities.Customer>();
        customer.FirstName.Should().Be("meri");
    }

    [Fact]
    public async void Post_ShouldReturnCustomer()
    {
        var (controller, repository, _, _, _) = Factory();
        repository.Setup(rp => rp.CreateCustomerAsync(It.IsAny<API.Entities.Customer>()))!.ReturnsAsync(Customers.FirstOrDefault());
        var actionResult = await controller.CreateCustomerAsync(new CreateCustomerModel());

        actionResult.Should().NotBeNull();
        actionResult.Result.Should().BeOfType<CreatedAtRouteResult>();
        var customer = actionResult.GetObjectResult();
        customer.Should().BeOfType<API.Entities.Customer>();
        customer.FirstName.Should().Be("meri");
    }

    [Fact]
    public async void Put_ShouldReturnCustomer()
    {
        var (controller, repository, _, _, publishEndPoint) = Factory();
        repository.Setup(rp => rp.GetCustomerAsync(It.IsAny<int>()))!.ReturnsAsync(Customers.FirstOrDefault());
        repository.Setup(rp => rp.UpdateCustomerAsync(It.IsAny<API.Entities.Customer>()))!.ReturnsAsync(true);
        publishEndPoint.Setup(p=>p.Publish(It.IsAny<UpdateCustomerEvent>(),It.IsAny<CancellationToken>())).Returns(Task.FromResult(default(UpdateCustomerEvent)));   
        var actionResult = await controller.UpdateCustomerAsync(new UpdateCustomerModel());

        actionResult.Should().NotBeNull();
        actionResult.Result.Should().BeOfType<OkObjectResult>();
        var customer = actionResult.GetObjectResult();
        customer.Should().Be(true);

    }


}