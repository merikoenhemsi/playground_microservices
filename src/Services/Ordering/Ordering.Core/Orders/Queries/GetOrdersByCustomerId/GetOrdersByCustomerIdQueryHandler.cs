using MediatR;
using Ordering.Core.Entities;
using Ordering.Core.Interfaces;
using Ordering.Core.Specification;

namespace Ordering.Core.Orders.Queries.GetOrdersByCustomerId;

public class GetOrdersByCustomerIdQueryHandler:IRequestHandler<GetOrdersByCustomerIdQuery,List<Order>>
{
    private readonly IAsyncRepository<Order> _orderRepository;

    public GetOrdersByCustomerIdQueryHandler(IAsyncRepository<Order> orderRepository)
    {
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
    }
    public async Task<List<Order>> Handle(GetOrdersByCustomerIdQuery request, CancellationToken cancellationToken)
    {
        return await _orderRepository.ListAsync(new OrdersByCustIdSpec(request.CustomerId), cancellationToken);
    }
}