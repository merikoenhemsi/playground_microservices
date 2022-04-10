using System.Runtime.Serialization;
using MediatR;
using Ordering.Core.Entities;

namespace Ordering.Core.Orders.Commands.CreateOrder;

public class CreateOrderCommand
    : IRequest<int>
{
    public int CustomerId { get; }
    public string CustomerName { get; }
    private readonly List<OrderItem> _orderItems;
    public List<OrderItem> OrderItems => _orderItems;

    public CreateOrderCommand()
    {
        _orderItems = new List<OrderItem>();
    }

    public CreateOrderCommand(List<OrderItem> orderItems, int customerId, string customerName) : this()
    {
        _orderItems = orderItems;
        CustomerId = customerId;
        CustomerName = customerName;
    }
}