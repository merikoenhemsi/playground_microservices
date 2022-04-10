using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Core.Entities;
using Ordering.Core.Orders.Commands.CancelOrder;
using Ordering.Core.Orders.Commands.CreateOrder;
using Ordering.Core.Orders.Queries.GetOrdersByDate;

namespace Ordering.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    [Route("betweenDates")]
    [ProducesResponseType(typeof(IEnumerable<Order>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<IEnumerable<Order>>> OrdersByDateAsync(DateTime startDate, DateTime endDate)
    {
        try
        {
            var query = new GetOrdersByDateQuery(startDate, endDate);
            var orders = await _mediator.Send(query);
            return Ok(orders);
        }
        catch 
        {
            return NotFound();
        }
    }

    [HttpPut]
    [Route("cancel/{id:int}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CancelOrderAsync(int id)
    {
        var command = new CancelOrderCommand
        {
            Id = id
        };

        bool commandResult = await _mediator.Send(command);

        if (!commandResult)
        {
            return BadRequest();
        }
        return Ok();
    }

    [HttpPost]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<int>> CreateOrderAsync([FromBody] CreateOrderCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

}