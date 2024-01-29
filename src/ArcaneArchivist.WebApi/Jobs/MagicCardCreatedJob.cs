using ArcaneArchivist.WebApi.Common.Messaging;
using ArcaneArchivist.WebApi.Entities.MagicCards;

namespace ArcaneArchivist.WebApi.Jobs;

public class MagicCardCreatedJob(ILogger<MagicCardCreatedJob> logger, IConsumerQueue queue) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var message = await queue.ReceiveAsync<MagicCard>();
            if (message != null) logger.LogInformation("Message received: {Message}", message);

            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        }
    }
}

public class TesteJob(ILogger<TesteJob> logger, IConsumerQueue queue) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var message = await queue.ReceiveAsync<string>();
            if (message != null) logger.LogInformation("Message received: {Message}", message);

            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        }
    }
}
