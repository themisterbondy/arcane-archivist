using MediatR;

namespace ArcaneArchivist.SharedKernel;

/// <summary>
///     Represents a domain event.
/// </summary>
public interface IDomainEvent : INotification;
