namespace EventBus.Messages.Events;

public class UpdateCustomerEvent:IntegrationBaseEvent
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string Gender { get; set; }
}