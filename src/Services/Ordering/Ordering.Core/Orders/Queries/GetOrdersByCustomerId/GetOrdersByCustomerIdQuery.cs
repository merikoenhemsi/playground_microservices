using MediatR;
using Ordering.Core.Entities;

namespace Ordering.Core.Orders.Queries.GetOrdersByCustomerId;

public class GetOrdersByCustomerIdQuery:IRequest<List<Order>>
{
    public int CustomerId { get; set; }

}