using ArcaneArchivist.WebApi.Common.Messaging;
using ArcaneArchivist.WebApi.Entities.MagicCards;

namespace ArcaneArchivist.WebApi.Jobs;

public class MagicCardCreatedJob(ILogger<MagicCardCreatedJob> logger, IQueue queueService)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var message = await queueService.ReceiveMessageAsync<MagicCard>("create-magic-cards");
            if (message != null) logger.LogInformation("Message received: {Message}", message);

            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        }
    }
}
