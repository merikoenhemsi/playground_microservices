using MediatR;
using Ordering.Core.Entities;
using Ordering.Core.Interfaces;

namespace Ordering.Core.Orders.Commands.CancelOrder;

public class CancelOrderCommandHandler:IRequestHandler<CancelOrderCommand,bool>
{
    private readonly IAsyncRepository<Order> _repository;

    public CancelOrderCommandHandler(IAsyncRepository<Order> repository)
    {
        _repository = repository;
    }
    public async Task<bool> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _repository.GetByIdAsync(request.Id,cancellationToken);
        if (order == null)
        {
            return false;
        }
        order.SetCancelledStatus();
        await _repository.UpdateAsync(order,cancellationToken);
        return true;
    }
}