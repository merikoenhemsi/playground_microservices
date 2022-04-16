using MediatR;
using Ordering.Core.Entities;
using Ordering.Core.Interfaces;
using Ordering.Core.Specification;

namespace Ordering.Core.Orders.Queries.GetOrdersByDate;

public class GetOrdersByDateQueryHandler : IRequestHandler<GetOrdersByDateQuery, List<Order>>
{
    private readonly IAsyncRepository<Order> _orderRepository;

    public GetOrdersByDateQueryHandler(IAsyncRepository<Order> orderRepository)
    {
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
    }
    public async Task<List<Order>> Handle(GetOrdersByDateQuery request, CancellationToken cancellationToken)
    {
       return await _orderRepository.ListAsync(new OrdersByDateSpec(request.StartDate, request.EndDate),cancellationToken);
    }
}