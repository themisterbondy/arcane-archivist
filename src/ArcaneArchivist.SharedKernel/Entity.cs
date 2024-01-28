namespace ArcaneArchivist.SharedKernel;

public class Entity
{
    /// <summary>
    ///     The domain events.
    /// </summary>
    private readonly List<IDomainEvent> _domainEvents = [];

    /// <summary>
    ///     Gets the domain events associated with this property.
    /// </summary>
    /// <returns>The collection of domain events.</returns>
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    /// <summary>
    ///     Adds the specified domain event to the list of domain events.
    /// </summary>
    /// <param name="domainEvent">The domain event to be added.</param>
    protected void Raise(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    /// <summary>
    ///     Clears the list of domain events.
    /// </summary>
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
