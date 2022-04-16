namespace EventBus.Messages.Events;

public class IntegrationBaseEvent
{
    public IntegrationBaseEvent()
    {
        CreatedDate = DateTime.UtcNow;
    }

    public IntegrationBaseEvent(int id, DateTime createdDate)
    {
        Id = id;
        CreatedDate = createdDate;
    }

    public int Id { get; private set; }

    public DateTime CreatedDate { get; private set; }
}