using MediatR;
using Ordering.Core.Entities;

namespace Ordering.Core.Orders.Commands.UpdateOrder;

public class UpdateOrderCommand:IRequest
{
    public Order Order { get; set; }
}