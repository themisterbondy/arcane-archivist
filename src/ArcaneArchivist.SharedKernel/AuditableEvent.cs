namespace ArcaneArchivist.SharedKernel;

public abstract record AuditableEvent
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
