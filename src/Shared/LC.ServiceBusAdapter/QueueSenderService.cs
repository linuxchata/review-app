using System;
using System.Threading.Tasks;

using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;

using ReviewApp.ServiceBusAdapter.Abstractions;

namespace ReviewApp.ServiceBusAdapter
{
    public sealed class QueueSenderService : IQueueSenderService
    {
        private readonly string connectionString;

        private readonly string queueName;

        private readonly ILogger<QueueSenderService> logger;

        private IQueueClient queueClient;

        public QueueSenderService(
            string connectionString,
            string queueName,
            ILogger<QueueSenderService> logger)
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

                await this.SendMessagesAsync(messageBody);

                await this.queueClient.CloseAsync();
            }
            catch (Exception exception)
            {
                this.logger.LogError("Error in queue client for {QueueName} queue: {Exception}", this.queueName, exception.Message);
                throw;
            }
        }

        private async Task SendMessagesAsync(byte[] messageBody)
        {
            try
            {
                var message = new Message(messageBody);
                this.logger.LogInformation("Sending message of {Length} bytes to the {QueueName} queue", messageBody.Length, this.queueName);

                await this.queueClient.SendAsync(message);
                this.logger.LogInformation("The message has been sent to the {QueueName} queue", this.queueName);
            }
            catch (Exception exception)
            {
                this.logger.LogError("Error sending message to the {QueueName} queue: {Exception}", this.queueName, exception.Message);
            }
        }
    }
}
