using ArcaneArchivist.WebApi.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ArcaneArchivist.WebApi.Persistence;

public class MagicCardDbContext(DbContextOptions<MagicCardDbContext> options) : DbContext(options)
{
    public DbSet<MagicCard> MagicCards { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MagicCardDbContext).Assembly);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<string>()
            .HaveMaxLength(255);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(
            Console.WriteLine,
            LogLevel.Information,
            DbContextLoggerOptions.DefaultWithLocalTime | DbContextLoggerOptions.None);
    }
}