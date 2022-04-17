using AutoMapper;
using Customer.API.Mapper;
using Customer.API.Models;
using EventBus.Messages.Events;
using FluentAssertions;
using Xunit;

namespace Customer.Tests.Mapper
{
    public class CustomerProfileTest
    {
        [Fact]
        public void Map_Customer_CreateCustomerModel_ShouldHaveValidConfig()
        {
            var configuration = new MapperConfiguration(cfg =>
                cfg.CreateMap<API.Entities.Customer, CreateCustomerModel>());

            configuration.AssertConfigurationIsValid();
        }

        [Fact]
        public void Map_Customer_UpdateCustomerModel_ShouldHaveValidConfig()
        {
            var configuration = new MapperConfiguration(cfg =>
                cfg.CreateMap<API.Entities.Customer, UpdateCustomerModel>());

            configuration.AssertConfigurationIsValid();
        }

        [Fact]
        public void Map_Customer_Customer_ShouldHaveValidConfig()
        {
            var configuration = new MapperConfiguration(cfg =>
                cfg.CreateMap<API.Entities.Customer, UpdateCustomerEvent>().ForMember(evt => evt.CustomerId,
                opt => opt.MapFrom(c => c.Id)));

            configuration.AssertConfigurationIsValid();
        }
    }

}

