using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ArcaneArchivist.WebApi.Tests.DependencyInjection;

public static class Helper
{
    private static IServiceProvider Provider()
    {
        var services = new ServiceCollection();

        services.AddDbContext<MagicCardDbContext>((provider, options) =>
        {
            options.UseInMemoryDatabase("Arcane", a =>
                a.EnableNullChecks(false));
            options.EnableSensitiveDataLogging();
        });

        return services.BuildServiceProvider();
    }

    public static T GetRequiredService<T>()
    {
        var provider = Provider();

        return provider.GetRequiredService<T>();
    }
}