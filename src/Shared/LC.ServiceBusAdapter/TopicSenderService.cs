using System;
using System.Threading.Tasks;

using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;

using ReviewApp.ServiceBusAdapter.Abstractions;

namespace ReviewApp.ServiceBusAdapter
{
    public sealed class TopicSenderService : ITopicSenderService
    {
        private readonly string connectionString;

        private readonly string topicName;

        private readonly ILogger<TopicSenderService> logger;

        private ITopicClient topicClient;

        public TopicSenderService(
            string connectionString,
            string topicName,
            ILogger<TopicSenderService> logger)
        {
            this.connectionString = connectionString;
            this.topicName = topicName;
            this.logger = logger;
        }

        public async Task SendMessageAsync(byte[] messageBody, string sendTo, string replyTo = null)
        {
            if (string.IsNullOrEmpty(sendTo))
            {
                throw new ArgumentNullException(nameof(sendTo), "Send To cannot be null");
            }

            try
            {
                this.topicClient = new TopicClient(this.connectionString, this.topicName);
                this.logger.LogInformation("Topic client for the {TopicName} topic has been created", this.topicName);

                await this.SendMessageInternalAsync(messageBody, sendTo, replyTo);

                await this.topicClient.CloseAsync();
            }
            catch (Exception exception)
            {
                this.logger.LogError("Error in queue client for the {TopicName} topic: {Exception}", this.topicName, exception.Message);
                throw;
            }
        }

        private async Task SendMessageInternalAsync(byte[] messageBody, string sendTo, string replyTo)
        {
            try
            {
                var message = new Message(messageBody)
                {
                    To = sendTo,
                    ReplyTo = replyTo
                };
                this.logger.LogInformation(
                    "Sending message to {To} of {Length} bytes to the {TopicName} topic",
                    message.To,
                    messageBody.Length,
                    this.topicName);

                await this.topicClient.SendAsync(message);
                this.logger.LogInformation("The message has been sent to the {TopicName} topic", this.topicName);
            }
            catch (Exception exception)
            {
                this.logger.LogError("Error sending message to the {TopicName} topic: {Exception}", this.topicName, exception.Message);
            }
        }
    }
}