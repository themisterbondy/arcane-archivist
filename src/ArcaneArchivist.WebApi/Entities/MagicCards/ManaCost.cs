namespace ArcaneArchivist.WebApi.Entities.MagicCards;

public class ManaCostColor
{
    public CardColor Color { get; set; }
    public int Quantity { get; set; }

    public static ManaCostColor Create(CardColor color, int quantity)
    {
        return new ManaCostColor
        {
            Color = color,
            Quantity = quantity
        };
    }
}

public class ManaCost
{
    public List<ManaCostColor> ManaColors { get; set; } = [];

    public static ManaCost Create(List<ManaCostColor> manaColors)
    {
        return new ManaCost
        {
            ManaColors = manaColors
        };
    }
}