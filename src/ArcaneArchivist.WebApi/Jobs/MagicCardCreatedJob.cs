using ArcaneArchivist.WebApi.Entities.MagicCards;
using ArcaneArchivist.WebApi.Messaging.MegicCard;

namespace ArcaneArchivist.WebApi.Jobs;

public class MagicCardCreatedJob(ILogger<MagicCardCreatedJob> logger, MegicCardCreatedQueue queue) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var message = await queue.ReceiveAsync<MagicCard>();
            if (message != null) logger.LogInformation("Message received: {Message}", message.Id);

            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        }
    }
}