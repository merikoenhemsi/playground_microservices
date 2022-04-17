using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Api.Models;
using Ordering.Core.Entities;
using Ordering.Core.Orders.Commands.CancelOrder;
using Ordering.Core.Orders.Queries.GetOrdersByCustomerId;
using Ordering.Core.Orders.Queries.GetOrdersByDate;
using System.Net;
using Ordering.Core.Orders.Commands.CreateOrder;

namespace Ordering.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public OrderController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper)); ;
    }

    [HttpGet]
    [Route("betweenDates")]
    [ProducesResponseType(typeof(IEnumerable<Order>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<IEnumerable<Order>>> OrdersByDateAsync(DateTime startDate, DateTime endDate)
    {
        try
        {
            if (startDate > endDate)
                return BadRequest();

            var query = new GetOrdersByDateQuery(startDate, endDate);
            var orders = await _mediator.Send(query);
            return Ok(orders);
        }
        catch 
        {
            return NotFound();
        }
    }

    [HttpGet]
    [Route("{customerId:int}")]
    [ProducesResponseType(typeof(IEnumerable<Order>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<IEnumerable<Order>>> OrdersByCustomerAsync(int customerId)
    {
        try
        {
            var query = new GetOrdersByCustomerIdQuery(){CustomerId = customerId};
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
        bool commandResult = false;

        if (id > 0)
        {
            var command = new CancelOrderCommand
            {
                Id = id
            };

            commandResult = await _mediator.Send(command);
        }

        if (!commandResult)
        {
            return BadRequest();
        }
        return Ok();
    }

    [HttpPost]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<int>> CreateOrderAsync([FromBody] CreateOrderModel model)
    {
        var command = _mapper.Map<CreateOrderCommand>(model);
        var result = await _mediator.Send(command);
        return Ok(result);
    }

}