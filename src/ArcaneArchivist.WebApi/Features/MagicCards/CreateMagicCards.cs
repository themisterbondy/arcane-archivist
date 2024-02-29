using ArcaneArchivist.SharedKernel;
using ArcaneArchivist.WebApi.Common.Extensions;
using ArcaneArchivist.WebApi.Contracts;
using ArcaneArchivist.WebApi.Entities.MagicCards;
using ArcaneArchivist.WebApi.Messaging.MegicCard;
using ArcaneArchivist.WebApi.Persistence;
using Carter;
using Carter.OpenApi;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CardColor = ArcaneArchivist.WebApi.Entities.MagicCards.CardColor;
using CardRarity = ArcaneArchivist.WebApi.Contracts.CardRarity;
using CardType = ArcaneArchivist.WebApi.Contracts.CardType;
using ManaCostColor = ArcaneArchivist.WebApi.Contracts.ManaCostColor;

namespace ArcaneArchivist.WebApi.Features;

public class CreateMagicCards
{
    public class Command : IRequest<Result<MagicCardResponse>>
    {
        public string Name { get; set; }
        public CardType Type { get; set; }
        public CardRarity Rarity { get; set; }
        public int Power { get; set; }
        public int Toughness { get; set; }
        public List<ManaCostColor> ManaColors { get; set; }
        public string? Description { get; set; }
        public string? Collection { get; set; }
        public string? Edition { get; set; }
        public int Quantity { get; set; }
        public string? ImageUrl { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithError(Error.Validation("Name", "O nome do ativo é obrigatório."));

            RuleFor(x => x.Type)
                .Must(Enum.IsDefined)
                .WithError(Error.Validation("Type", "O tipo do ativo é obrigatório."));

            RuleFor(x => x.Rarity)
                .Must(Enum.IsDefined)
                .WithError(Error.Validation("Rarity", "A raridade do ativo é obrigatória."));

            RuleFor(x => x.Power)
                .NotEmpty()
                .WithError(Error.Validation("Power", "O poder do ativo é obrigatório."));

            RuleFor(x => x.Toughness)
                .NotEmpty()
                .WithError(Error.Validation("Toughness", "A resistência do ativo é obrigatória."));

            RuleFor(x => x.ManaColors)
                .NotEmpty()
                .WithError(Error.Validation("ManaColors", "A cor do ativo é obrigatória."));

            RuleFor(x => x.Quantity)
                .NotEmpty()
                .WithError(Error.Validation("Quantity", "A quantidade do ativo é obrigatória."));
        }
    }

    public class Handler(MagicCardDbContext context, MegicCardCreatedQueue queue, ILogger<Handler> logger)
        : IRequestHandler<Command, Result<MagicCardResponse>>
    {
        public async Task<Result<MagicCardResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var manaColors = request.ManaColors.Select(x =>
                Entities.MagicCards.ManaCostColor.Create(
                    (CardColor)x.Color, x.Quantity)).ToList();

            var magicCard = MagicCard.Create(MagicCardId.New(), request.Name,
                (Entities.MagicCards.CardType)request.Type, (Entities.MagicCards.CardRarity)request.Rarity,
                request.Power, request.Toughness, ManaCost.Create(manaColors), request.Quantity);

            await context.MagicCards.AddAsync(magicCard, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);

            await queue.PublishAsync(magicCard);

            return Result.Success(MagicCardsTools.BuildResponse(magicCard));
        }
    }
}

public class CreateMagicCardsModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/magic-cards",
                async ([FromServices] ISender sender, [FromBody] MagicCardRequest request) =>
                {
                    var command = request.Adapt<CreateMagicCards.Command>();

                    var result = await sender.Send(command);

                    return result.IsSuccess
                        ? Results.Created($"api/magic-cards/{result.Value.Id}", result.Value)
                        : result.ToProblemDetails();
                })
            .Accepts<MagicCardRequest>("application/json")
            .Produces<MagicCardResponse>()
            .WithTags("Magic Cards")
            .WithName("CreateMagicCards")
            .IncludeInOpenApi();
    }
}
