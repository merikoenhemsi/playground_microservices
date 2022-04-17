using AutoMapper;
using EventBus.Messages.Events;
using Ordering.Api.Models;
using Ordering.Core.Entities;
using Ordering.Core.Orders.Commands.CreateOrder;
using Ordering.Core.Orders.Models;

namespace Ordering.Api.Mapper;

public class OrderingProfile:Profile
{
    public OrderingProfile()
    {
        CreateMap<UpdateCustomerEvent, UpdateCustomerModel>();
        CreateMap<CreateOrderModel, CreateOrderCommand>();
        CreateMap<OrderItemModel, OrderItem>();
    }
}