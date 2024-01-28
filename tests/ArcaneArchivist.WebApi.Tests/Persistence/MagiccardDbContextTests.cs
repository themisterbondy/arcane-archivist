using ArcaneArchivist.WebApi.Entities.MagicCards;

namespace ArcaneArchivist.WebApi.Tests.Persistence;

public class MagiccardDbContextTests
{
    private readonly MagicCardDbContext _context = Helper.GetRequiredService<MagicCardDbContext>();

    [Fact]
    public void CreateMagicCard_Succeeds()
    {
        // Arrange
        var id = MagicCardId.New();
        var name = "Test Card";
        var type = CardType.Creature;
        var rarity = CardRarity.Common;
        var power = 5;
        var toughness = 5;
        var manaCost = ManaCost.Create([ManaCostColor.Create(CardColor.Red, 2)]);
        var quantity = 10;

        // Act
        var card = MagicCard.Create(id, name, type, rarity, power, toughness,
            manaCost, quantity
        );

        _context.MagicCards.Add(card);
        _context.SaveChanges();

        var result = _context.MagicCards.Find(id);

        // Assert
        _context.MagicCards.Should().Contain(card);
        result.Should().BeEquivalentTo(card);
    }
}