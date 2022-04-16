using AutoMapper;
using EventBus.Messages.Events;
using Ordering.Core.Orders.Models;

namespace Ordering.Api.Mapper;

public class OrderingProfile:Profile
{
    public OrderingProfile()
    {
        CreateMap<UpdateCustomerEvent, UpdateCustomerModel>();
    }
}