using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Core.Entities;
using Ordering.Core.Interfaces;

namespace Ordering.Core.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
{
    private readonly IAsyncRepository<Order> _repository;
    private readonly ILogger<CreateOrderCommandHandler> _logger;

    public CreateOrderCommandHandler(IAsyncRepository<Order> repository, ILogger<CreateOrderCommandHandler> logger)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger)); ;
    }
    public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order= await _repository.AddAsync(new Order
        {
            CustomerId = request.CustomerId,
            CustomerName = request.CustomerName,
            OrderItems = request.OrderItems
        });

        _logger.LogInformation($"Order is created.");
        return order.Id;
    }
}