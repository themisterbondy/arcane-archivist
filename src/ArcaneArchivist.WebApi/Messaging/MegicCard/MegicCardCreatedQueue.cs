using ArcaneArchivist.WebApi.Common.Messaging;

namespace ArcaneArchivist.WebApi.Messaging.MegicCard;

public interface IMegicCardCreatedQueue : IPublisherQueue, IConsumerQueue
{
}

public class MegicCardCreatedQueue(IQueue queue) : IMegicCardCreatedQueue
{
    private readonly string queueName = "created-magic-cards";

    public async Task PublishAsync<T>(T message)
    {
        await queue.SendMessageAsync(queueName, message);
    }

    public async Task<T> ReceiveAsync<T>()
    {
        return await queue.ReceiveMessageAsync<T>(queueName);
    }
}

public interface ITesteQueue : IPublisherQueue, IConsumerQueue
{
}

public class TesteQueue(IQueue queue) : ITesteQueue
{
    private readonly string queueName = "teste";

    public async Task PublishAsync<T>(T message)
    {
        await queue.SendMessageAsync(queueName, message);
    }

    public async Task<T> ReceiveAsync<T>()
    {
        return await queue.ReceiveMessageAsync<T>(queueName);
    }
}
