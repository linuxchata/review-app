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
                this.logger.LogInformation("Queue client for {queuename} has been created", this.queueName);

                await SendMessagesAsync(messageBody);
                this.logger.LogInformation("The message has been sent");

                await this.queueClient.CloseAsync();
            }
            catch (Exception exception)
            {
                this.logger.LogError("Error in queue client for {queuename}: {exception}", this.queueName, exception);
                throw;
            }
        }

        private async Task SendMessagesAsync(byte[] messageBody)
        {
            try
            {
                var message = new Message(messageBody);

                this.logger.LogInformation("Sending message");
                await this.queueClient.SendAsync(message);
            }
            catch (Exception exception)
            {
                this.logger.LogError("Error sending message to the {queuename}: {exception}", this.queueName, exception);
            }
        }
    }
}
