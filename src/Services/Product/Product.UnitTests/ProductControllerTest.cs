using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Product.API.Controllers;
using Product.API.Repositories;
using Product.UnitTests.Extensions;
using Xunit;

namespace Product.UnitTests
{
    public class ProductControllerTest
    {

        private static readonly List<API.Entities.Product> Products
            = new()
            {
                new API.Entities.Product()
                {
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Name="productName"
                },
                new API.Entities.Product()
                {
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                }
            };

        private static (ProductController, Mock<IAsyncRepository<API.Entities.Product>>, Mock<ILogger<ProductController>>) Factory()
        {
            var logger = new Mock<ILogger<ProductController>>();
            var repository = new Mock<IAsyncRepository<API.Entities.Product>>();

            var controller = new ProductController(repository.Object, logger.Object);
            return (controller, repository, logger);
        }

        [Fact]
        public async void Get_ShouldReturnCustomers()
        {
            var (controller, repository,_) = Factory();
            repository.Setup(rp => rp.GetAllAsync()).ReturnsAsync(Products);
            var actionResult = await controller.ProductsAsync();

            actionResult.Should().NotBeNull();
            actionResult.Result.Should().BeOfType<OkObjectResult>();
            var products = actionResult.GetObjectResult();
            products.Should().BeOfType<List<API.Entities.Product>>();
            products.Count.Should().Be(2);
        }

        [Fact]
        public async void Get_ShouldReturnCustomer()
        {
            var (controller, repository, _) = Factory();
            repository.Setup(rp => rp.GetByIdAsync(It.IsAny<int>()))!.ReturnsAsync(Products.FirstOrDefault());
            var actionResult = await controller.ProductByIdAsync(5);

            actionResult.Should().NotBeNull();
            actionResult.Result.Should().BeOfType<OkObjectResult>();
            var product = actionResult.GetObjectResult();
            product.Should().BeOfType<API.Entities.Product>();
            product.Name.Should().Be("productName");
        }
    }
}