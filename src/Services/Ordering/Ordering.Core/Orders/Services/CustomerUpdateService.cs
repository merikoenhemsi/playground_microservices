using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Core.Interfaces;
using Ordering.Core.Orders.Commands.UpdateOrder;
using Ordering.Core.Orders.Models;
using Ordering.Core.Orders.Queries.GetOrdersByCustomerId;

namespace Ordering.Core.Orders.Services;

public class CustomerUpdateService:ICustomerUpdateService
{
    private readonly IMediator _mediator;
    private readonly ILogger<CustomerUpdateService> _logger;

    public CustomerUpdateService(IMediator mediator, ILogger<CustomerUpdateService> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public async void UpdateCustomerNameInOrders(UpdateCustomerModel request)
    {
        try
        {
            var orders = await _mediator.Send(new GetOrdersByCustomerIdQuery()
            {
                CustomerId = request.Id
            });

            if (orders.Count != 0)
            {
                orders.ForEach(x => x.CustomerName = $"{request.FirstName} {request.LastName}");
            }

            orders.ForEach(async o => await _mediator.Send(new UpdateOrderCommand() { Order = o }));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception: }", nameof(CustomerUpdateService));
        }
    }
}