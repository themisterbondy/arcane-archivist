using ArcaneArchivist.SharedKernel;

namespace ArcaneArchivist.WebApi.Entities.MagicCards;

public record MagicCardCreatedDomainEvent(MagicCardId MagicCardId) : AuditableEvent, IDomainEvent;
