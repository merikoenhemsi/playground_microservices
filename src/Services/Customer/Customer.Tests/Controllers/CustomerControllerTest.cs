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

    private static (CustomerController, Mock<IAsyncRepository<API.Entities.Customer>>, Mock<ILogger<CustomerController>>, Mock<IMapper>, Mock<IPublishEndpoint>) Factory()
    {
        var logger = new Mock<ILogger<CustomerController>>();
        var repository = new Mock<IAsyncRepository<API.Entities.Customer>>();
        var mapper = new Mock<IMapper>();
        var publishEndPoint = new Mock<IPublishEndpoint>();

        var controller = new CustomerController(repository.Object, logger.Object, mapper.Object, publishEndPoint.Object);
        return (controller, repository, logger, mapper, publishEndPoint);
    }

    [Fact]
    public async void Get_ShouldReturnCustomers()
    {
        var (controller, repository, _, _, _) = Factory();
        repository.Setup(rp => rp.GetAllAsync()).ReturnsAsync(Customers);
        var actionResult = await controller.CustomersAsync();
       
        actionResult.Should().NotBeNull();
        actionResult.Result.Should().BeOfType<OkObjectResult>();
        var customers = actionResult.GetObjectResult();
        customers.Should().BeOfType<List<API.Entities.Customer>>();
        customers.Count.Should().Be(2);
    }

    [Fact]
    public async void Get_ShouldReturnCustomer()
    {
        var (controller, repository, _, _, _) = Factory();
        repository.Setup(rp => rp.GetByIdAsync(It.IsAny<int>()))!.ReturnsAsync(Customers.FirstOrDefault());
        var actionResult = await controller.CustomerByIdAsync(5);

        actionResult.Should().NotBeNull();
        actionResult.Result.Should().BeOfType<OkObjectResult>();
        var customer = actionResult.GetObjectResult();
        customer.Should().BeOfType<API.Entities.Customer>();
        customer.FirstName.Should().Be("meri");
    }

    [Fact]
    public async void Get_ShouldReturnBadRequest()
    {
        var (controller, repository, _, _, _) = Factory();
        repository.Setup(rp => rp.GetByIdAsync(It.IsAny<int>()))!.ReturnsAsync(Customers.FirstOrDefault());
        var actionResult = await controller.CustomerByIdAsync(0);

        actionResult.Should().NotBeNull();
        actionResult.Result.Should().BeOfType<BadRequestResult>();
    }

    [Fact]
    public async void Post_ShouldReturnCustomer()
    {
        var (controller, repository, _, _, _) = Factory();
        repository.Setup(rp => rp.AddAsync(It.IsAny<API.Entities.Customer>()))!.ReturnsAsync(Customers.FirstOrDefault());
        var actionResult = await controller.CreateCustomerAsync(new CreateCustomerModel());

        actionResult.Should().NotBeNull();
        actionResult.Result.Should().BeOfType<CreatedAtRouteResult>();
        var customer = actionResult.GetObjectResult();
        customer.Should().BeOfType<API.Entities.Customer>();
        customer.FirstName.Should().Be("meri");
    }

    [Fact]
    public async void Post_ShouldReturnBadRequest()
    {
        var (controller, repository, _, _, _) = Factory();
        repository.Setup(rp => rp.AddAsync(It.IsAny<API.Entities.Customer>())).Throws(new Exception("Boom"));
        var actionResult = await controller.CreateCustomerAsync(new CreateCustomerModel());

        actionResult.Should().NotBeNull();
        actionResult.Result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async void Put_ShouldReturnOkWithPublish()
    {
        var (controller, repository, _, _, publishEndPoint) = Factory();
        repository.Setup(rp => rp.GetByIdAsync(It.IsAny<int>()))!.ReturnsAsync(Customers.FirstOrDefault());
        repository.Setup(rp => rp.UpdateAsync(It.IsAny<API.Entities.Customer>()))!.Returns(Task.FromResult(true));
        publishEndPoint.Setup(p=>p.Publish(It.IsAny<UpdateCustomerEvent>(),It.IsAny<CancellationToken>())).Returns(Task.FromResult(default(UpdateCustomerEvent)));   
        var actionResult = await controller.UpdateCustomerAsync(new UpdateCustomerModel());

        actionResult.Should().NotBeNull(); 
        actionResult.Should().BeOfType<OkResult>();
    }

    [Fact]
    public async void Put_ShouldReturnOkWithNoPublish()
    {
        var customer = Customers.FirstOrDefault();
        var (controller, repository, _, _, publishEndPoint) = Factory();
        repository.Setup(rp => rp.GetByIdAsync(It.IsAny<int>()))!.ReturnsAsync(customer);
        repository.Setup(rp => rp.UpdateAsync(It.IsAny<API.Entities.Customer>()))!.Returns(Task.FromResult(true));
        var actionResult = await controller.UpdateCustomerAsync(new UpdateCustomerModel
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName
            }
        );

        actionResult.Should().NotBeNull();
        actionResult.Should().BeOfType<OkResult>();
    }

    [Fact]
    public async void Put_ShouldReturnBadRequest()
    {
        var (controller, repository, _, _, _) = Factory();
        repository.Setup(rp => rp.GetByIdAsync(It.IsAny<int>())).Throws(new Exception("Boom"));
        var actionResult = await controller.UpdateCustomerAsync(new UpdateCustomerModel());

        actionResult.Should().NotBeNull(); 
        actionResult.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async void Delete_ShouldReturnCustomer()
    {
        var (controller, repository, _, _, _) = Factory();
        repository.Setup(rp => rp.DeleteAsync(It.IsAny<API.Entities.Customer>())).Returns(Task.FromResult(true));
        var actionResult = await controller.DeleteCustomerByIdAsync(5);

        actionResult.Should().NotBeNull();
        actionResult.Should().BeOfType<NoContentResult>();
    }


}