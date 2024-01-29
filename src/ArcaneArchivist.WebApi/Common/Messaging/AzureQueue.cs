using ArcaneArchivist.WebApi.Common.Messaging;
using Azure.Storage.Queues;
using Newtonsoft.Json;

namespace ArcaneArchivist.WebApi.Services;

public class AzureQueue(QueueServiceClient queueServiceClient) : IQueue
{
    public async Task SendMessageAsync<T>(string queueName, T message)
    {
        try
        {
            var serializedMessage = JsonConvert.SerializeObject(message);
            var queueClient = queueServiceClient.GetQueueClient(queueName);
            await queueClient.CreateIfNotExistsAsync();
            await queueClient.SendMessageAsync(serializedMessage);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error sending message to queue: " + e.Message);
        }
    }

    public async Task<T> ReceiveMessageAsync<T>(string queueName)
    {
        try
        {
            var queueClient = queueServiceClient.GetQueueClient(queueName);
            var response = await queueClient.ReceiveMessagesAsync();
            if (response.Value.Length > 0)
            {
                var message = response.Value[0];
                //await queueClient.DeleteMessageAsync(message.MessageId, message.PopReceipt);

                var deserializedMessage = JsonConvert.DeserializeObject<T>(message.MessageText);
                return deserializedMessage;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Error receiving message from queue: " + e.Message);
        }

        return default;
    }
}
