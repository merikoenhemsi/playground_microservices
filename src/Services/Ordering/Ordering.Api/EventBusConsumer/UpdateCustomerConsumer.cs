using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using Ordering.Core.Interfaces;
using Ordering.Core.Orders.Models;

namespace Ordering.Api.EventBusConsumer
{
    public class UpdateCustomerConsumer:IConsumer<UpdateCustomerEvent>
    {
        private readonly ICustomerUpdateService _service;
        private readonly IMapper _mapper;

        public UpdateCustomerConsumer(ICustomerUpdateService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<UpdateCustomerEvent> context)
        {
            var updatedOrder=_mapper.Map<UpdateCustomerModel>(context.Message);
            await _service.UpdateCustomerNameInOrders(updatedOrder);
        }
    }
}
