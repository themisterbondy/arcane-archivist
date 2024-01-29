using ArcaneArchivist.WebApi.Contracts;
using ArcaneArchivist.WebApi.Entities.MagicCards;
using CardColor = ArcaneArchivist.WebApi.Contracts.CardColor;
using CardRarity = ArcaneArchivist.WebApi.Contracts.CardRarity;
using CardType = ArcaneArchivist.WebApi.Contracts.CardType;
using ManaCostColor = ArcaneArchivist.WebApi.Contracts.ManaCostColor;

namespace ArcaneArchivist.WebApi.Features;

public static class MagicCardsTools
{
    public static MagicCardResponse BuildResponse(MagicCard magicCard)
    {
        return new MagicCardResponse
        {
            Id = magicCard.Id.Value,
            Name = magicCard.Name,
            Type = (CardType)magicCard.Type,
            Rarity = (CardRarity)magicCard.Rarity,
            Power = magicCard.Power,
            Toughness = magicCard.Toughness,
            ManaColors = magicCard.ManaCost.ManaColors.Select(x =>
                new ManaCostColor
                {
                    Color = (CardColor)x.Color,
                    Quantity = x.Quantity
                }).ToList(),
            Description = magicCard.Description,
            Collection = magicCard.Collection,
            Edition = magicCard.Edition,
            Quantity = magicCard.Quantity,
            ImageUrl = magicCard.ImageUrl,
            CreatedAt = magicCard.CreatedAt
        };
    }
}
