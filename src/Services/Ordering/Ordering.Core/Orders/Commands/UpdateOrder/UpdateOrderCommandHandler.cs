using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Core.Entities;
using Ordering.Core.Interfaces;

namespace Ordering.Core.Orders.Commands.UpdateOrder;

public class UpdateOrderCommandHandler:IRequestHandler<UpdateOrderCommand>
{
    private readonly IAsyncRepository<Order> _repository;
    private readonly ILogger<UpdateOrderCommandHandler> _logger;

    public UpdateOrderCommandHandler(IAsyncRepository<Order> repository, ILogger<UpdateOrderCommandHandler> logger)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        await _repository.UpdateAsync(request.Order,cancellationToken);
        _logger.LogInformation($"Order is updated.");
        return Unit.Value;
    }
}