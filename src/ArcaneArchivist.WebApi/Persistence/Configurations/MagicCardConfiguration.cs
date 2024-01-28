using ArcaneArchivist.WebApi.Entities.MagicCards;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArcaneArchivist.WebApi.Persistence.Configurations;

public class MagicCardConfiguration : IEntityTypeConfiguration<MagicCard>
{
    public void Configure(EntityTypeBuilder<MagicCard> builder)
    {
        builder.HasKey(mc => mc.Id);

        builder.Property(e => e.Id)
            .HasConversion(assetId => assetId.Value,
                value => new MagicCardId(value));

        builder.Property(mc => mc.Name)
            .IsRequired();

        builder.Property(mc => mc.Type)
            .IsRequired();

        builder.Property(mc => mc.Rarity)
            .IsRequired();

        builder.Property(mc => mc.Power);

        builder.Property(mc => mc.Toughness);

        builder.OwnsOne(mc => mc.ManaCost, builder =>
        {
            builder.ToJson();
            builder.OwnsMany(contactDetails => contactDetails.ManaColors);
        });

        builder.Property(mc => mc.Description);

        builder.Property(mc => mc.Collection);

        builder.Property(mc => mc.Edition);

        builder.Property(mc => mc.Quantity);

        builder.Property(mc => mc.ImageUrl);

        builder.Property(mc => mc.CreatedAt)
            .IsRequired();
    }
}