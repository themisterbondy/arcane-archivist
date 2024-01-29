using ArcaneArchivist.WebApi.Common.Messaging;

namespace ArcaneArchivist.WebApi.Messaging.MegicCard;

public class MegicCardCreatedQueue(IQueue queue)
{
    private readonly string queueName = "created-magic-cards";

    public async Task PublishAsync<T>(T message)
    {
        await queue.PublishMessageAsync(queueName, message);
    }

    public async Task<T> ReceiveAsync<T>()
    {
        return await queue.ReceiveMessageAsync<T>(queueName);
    }
}
