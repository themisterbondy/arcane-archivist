namespace ArcaneArchivist.SharedKernel;

public class DomainEvent
{
    public Guid EventId { get; private set; } = Guid.NewGuid();
    public string? Content { get; protected set; }
    public DateTime CreatedAt { get; protected set; }
}