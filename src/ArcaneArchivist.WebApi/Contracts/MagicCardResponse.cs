namespace ArcaneArchivist.WebApi.Contracts;

public class MagicCardRequest
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

public record MagicCardResponse
{
    public Guid Id { get; set; }
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
    public DateTime CreatedAt { get; set; }
}

public class ManaCostColor
{
    public CardColor Color { get; set; }
    public int Quantity { get; set; }
}

public enum CardType
{
    Creature,
    Spell,
    Artifact,
    Enchantment,
    Planeswalker,
    Land
}

public enum CardRarity
{
    Common,
    Uncommon,
    Rare,
    MythicRare
}

public enum CardColor
{
    White,
    Blue,
    Black,
    Red,
    Green,
    Colorless
}
