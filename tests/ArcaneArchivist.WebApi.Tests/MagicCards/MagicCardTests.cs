using ArcaneArchivist.WebApi.Entities.MagicCards;

namespace ArcaneArchivist.WebApi.Tests;

public class MagicCardTests
{
    [Fact]
    public void MagicCard_Creation_WithValidParameters_Succeeds()
    {
        // Arrange
        var id = MagicCardId.New();
        var name = "Test Card";
        var type = CardType.Creature;
        var rarity = CardRarity.Common;
        var power = 5;
        var toughness = 5;
        var manaCost =
            ManaCost.Create([ManaCostColor.Create(CardColor.Red, 2), ManaCostColor.Create(CardColor.Blue, 1)]);
        var quantity = 10;

        // Act
        var card = MagicCard.Create(id, name, type, rarity, power, toughness,
            manaCost, quantity
        );

        // Assert
        card.Should().BeEquivalentTo(new
        {
            Id = id,
            Name = name,
            Type = type,
            Rarity = rarity,
            Power = power,
            Toughness = toughness,
            ManaCost = manaCost,
            Quantity = quantity
        });
    }

    [Fact]
    public void MagicCard_Creation_WithValidParametersAndAddAdditionalProperties_Succeeds()
    {
        // Arrange
        var id = MagicCardId.New();
        var name = "Test Card";
        var type = CardType.Creature;
        var rarity = CardRarity.Common;
        var power = 5;
        var toughness = 5;
        var manaCost = ManaCost.Create([ManaCostColor.Create(CardColor.Red, 2)]);
        var description = "Test Description";
        var collection = "Test Collection";
        var edition = "Test Edition";
        var quantity = 10;
        var imageUrl = "https://test.com/image.jpg";

        // Act
        var card = MagicCard.Create(id, name, type, rarity, power, toughness,
            manaCost, quantity
        );

        card.Description = description;
        card.Collection = collection;
        card.Edition = edition;
        card.ImageUrl = imageUrl;

        // Assert
        card.Should().BeEquivalentTo(new
        {
            Id = id,
            Name = name,
            Type = type,
            Rarity = rarity,
            Power = power,
            Toughness = toughness,
            ManaCost = manaCost,
            Quantity = quantity,
            Description = description,
            Collection = collection,
            Edition = edition,
            ImageUrl = imageUrl
        });
    }
}