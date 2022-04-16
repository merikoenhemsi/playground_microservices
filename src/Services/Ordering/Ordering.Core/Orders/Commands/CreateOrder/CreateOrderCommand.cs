using MediatR;
using Ordering.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Ordering.Core.Orders.Commands.CreateOrder;

public class CreateOrderCommand
    : IRequest<int>
{
    public int? CustomerId { get; }
    public string CustomerName { get; }

    private readonly List<OrderItem> _orderItems;
    
    [Required]
    public List<OrderItem> OrderItems => _orderItems;

    public CreateOrderCommand(int? customerId, string customerName, List<OrderItem> orderItems)
    {
        CustomerId = customerId;
        CustomerName = customerName;
        _orderItems = orderItems;
    }
}