namespace ArcaneArchivist.WebApi.Common.Messaging;

public interface IQueue
{
    Task SendMessageAsync<T>(string queueName, T message);
    Task<T> ReceiveMessageAsync<T>(string queueName);
}
