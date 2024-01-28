namespace ArcaneArchivist.SharedKernel;

/// <summary>
///     The OutboxMessage class represents an outbox message that needs to be sent.
/// </summary>
public class OutboxMessage
{
    public Guid Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime OccuredOnUtc { get; set; }
    public DateTime? ProcessedOnUtc { get; set; }
    public string? Error { get; set; }
}