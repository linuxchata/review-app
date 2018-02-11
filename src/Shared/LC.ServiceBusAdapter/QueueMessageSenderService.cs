using System;
using System.Threading.Tasks;
using LC.ServiceBusAdapter.Abstractions;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;

namespace LC.ServiceBusAdapter
{
    public sealed class QueueMessageSenderService : IQueueMessageSenderService
    {
        private readonly string connectionString;

        private readonly string queueName;

        private readonly ILogger logger;

        private IQueueClient queueClient;

        public QueueMessageSenderService(
            string connectionString,
            string queueName,
            ILogger<QueueMessageSenderService> logger)
        {
            this.connectionString = connectionString;
            this.queueName = queueName;
            this.logger = logger;
        }

        public async Task SendMessage(byte[] messageBody)
        {
            try
            {
                this.queueClient = new QueueClient(this.connectionString, this.queueName);
                this.logger.LogInformation("Queue client for {QueueName} queue has been created", this.queueName);

                await SendMessagesAsync(messageBody);

                await this.queueClient.CloseAsync();
            }
            catch (Exception exception)
            {
                this.logger.LogError("Error in queue client for {QueueName} queue: {Exception}", this.queueName, exception);
                throw;
            }
        }

        private async Task SendMessagesAsync(byte[] messageBody)
        {
            try
            {
                var message = new Message(messageBody);

                this.logger.LogInformation("Sending message to the {QueueName} queue", this.queueName);

                await this.queueClient.SendAsync(message);
                this.logger.LogInformation("The message has been sent to the {QueueName} queue", this.queueName);
            }
            catch (Exception exception)
            {
                this.logger.LogError("Error sending message to the {QueueName} queue: {Exception}", this.queueName, exception);
            }
        }
    }
}
