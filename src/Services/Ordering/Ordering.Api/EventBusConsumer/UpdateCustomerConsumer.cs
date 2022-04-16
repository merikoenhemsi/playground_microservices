using EventBus.Messages.Events;
using MassTransit;

namespace Ordering.Api.EventBusConsumer
{
    public class UpdateCustomerConsumer:IConsumer<UpdateCustomerEvent>
    {
        public Task Consume(ConsumeContext<UpdateCustomerEvent> context)
        {
            return null;
        }
    }
}
