using MediatR;

namespace Ordering.Core.Orders.Commands.CancelOrder;

public class CancelOrderCommand:IRequest<bool>
{
    public int Id { get; set; }
}