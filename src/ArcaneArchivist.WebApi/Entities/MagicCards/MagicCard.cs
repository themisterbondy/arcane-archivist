using ArcaneArchivist.SharedKernel;

namespace ArcaneArchivist.WebApi.Entities.MagicCards;

public record MagicCardId(Guid Value)
{
    public static MagicCardId New()
    {
        return new MagicCardId(Guid.NewGuid());
    }
}

public class MagicCard : Entity
{
    public MagicCardId Id { get; set; }
    public string Name { get; set; } // Nome da carta

    public CardType Type { get; set; } // Tipo da carta (criatura, feitiço, etc.)
    public CardRarity Rarity { get; set; } // Raridade da carta (comum, rara, etc.)
    public int Power { get; set; } // Poder da criatura
    public int Toughness { get; set; } // Resistência da criatura
    public ManaCost ManaCost { get; set; } // Custo de mana da carta
    public string? Description { get; set; } // Descrição da carta
    public string? Collection { get; set; } // Coleção da qual a carta faz parte
    public string? Edition { get; set; } // Edição da carta
    public int Quantity { get; set; } // Quantidade em estoque da carta
    public string? ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public static MagicCard Create(MagicCardId id, string name, CardType type, CardRarity rarity, int power,
        int toughness, ManaCost manaCost, int quantity)
    {
        var magicCard = new MagicCard
        {
            Id = id,
            Name = name,
            Type = type,
            Rarity = rarity,
            Power = power,
            Toughness = toughness,
            ManaCost = manaCost,
            Quantity = quantity
        };

        magicCard.Raise(new MagicCardCreatedDomainEvent(magicCard.Id));

        return magicCard;
    }
}
