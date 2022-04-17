namespace EventBus.Messages.Events;

public class UpdateCustomerEvent:IntegrationBaseEvent
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public int CustomerId { get; set; }
}