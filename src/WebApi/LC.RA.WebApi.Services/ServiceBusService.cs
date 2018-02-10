using System;
using System.Text;
using System.Threading.Tasks;
using LC.RA.WebApi.Services.Contracts;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;

namespace LC.RA.WebApi.Services
{
    public sealed class ServiceBusService : IServiceBusService
    {
        private const string ServiceBusConnectionString = @"https://ReviewApp.servicebus.windows.net";

        private const string QueueName = "reviewapp";

        private IQueueClient queueClient;

        private readonly ILogger logger;

        public ServiceBusService(ILogger<ServiceBusService> logger)
        {
            this.logger = logger;
        }

        public async Task SendMessage()
        {
            const int numberOfMessages = 10;

            this.queueClient = new QueueClient(ServiceBusConnectionString, QueueName);
            this.logger.LogInformation("Queue client for {queuename} has been created", QueueName);

            // Send messages.
            await SendMessagesAsync(numberOfMessages);

            await this.queueClient.CloseAsync();
        }

        private async Task SendMessagesAsync(int numberOfMessagesToSend)
        {
            try
            {
                for (var i = 0; i < numberOfMessagesToSend; i++)
                {
                    // Create a new message to send to the queue.
                    var messageBody = $"Message {i}";
                    var message = new Message(Encoding.UTF8.GetBytes(messageBody));

                    this.logger.LogInformation("Sending message {messageBody}", messageBody);
                    await this.queueClient.SendAsync(message);
                }
            }
            catch (Exception exception)
            {
                this.logger.LogError("Error sending message: {exception}", exception);
            }
        }
    }
}