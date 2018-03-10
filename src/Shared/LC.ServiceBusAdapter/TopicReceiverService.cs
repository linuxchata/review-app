using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LC.ServiceBusAdapter.Abstractions;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;

namespace LC.ServiceBusAdapter
{
    public sealed class TopicReceiverService : ITopicReceiverService
    {
        private readonly string connectionString;

        private readonly string topicName;

        private readonly string subscriptionName;

        private readonly IMessageHandler messageHandler;

        private readonly ILogger<TopicReceiverService> logger;

        private ISubscriptionClient subscriptionClient;

        public TopicReceiverService(
            string connectionString,
            string topicName,
            string subscriptionName,
            IMessageHandler messageHandler,
            ILogger<TopicReceiverService> logger)
        {
            this.connectionString = connectionString;
            this.topicName = topicName;
            this.subscriptionName = subscriptionName;
            this.messageHandler = messageHandler;
            this.logger = logger;
        }

        public async Task ReceiveMessagesAsync(string receiver, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(receiver))
            {
                throw new ArgumentNullException(nameof(receiver), "Receiver cannot be null");
            }

            try
            {
                this.subscriptionClient = new SubscriptionClient(this.connectionString, this.topicName, this.subscriptionName);
                this.logger.LogInformation("Subscription client for the {TopicName} topic has been created", this.topicName);

                await this.HandlerSubscriptionRules(receiver);

                this.RegisterOnMessageHandlerAndReceiveMessages();

                await Task.Delay(-1, cancellationToken);

                await this.subscriptionClient.CloseAsync();
            }
            catch (Exception exception)
            {
                this.logger.LogError("Error in queue client for the {TopicName} topic: {Exception}", this.topicName, exception.Message);
                throw;
            }
        }

        private async Task HandlerSubscriptionRules(string receiver)
        {
            var rules = (await this.subscriptionClient.GetRulesAsync()).ToList();
            var defaultRule = rules.FirstOrDefault(a => string.Equals(a.Name, RuleDescription.DefaultRuleName));
            if (defaultRule != null)
            {
                await this.subscriptionClient.RemoveRuleAsync(defaultRule.Name);
            }

            var subscriptionRuleName = string.Format("{0}Rule", receiver);
            var subscriptionRule = rules.FirstOrDefault(a => string.Equals(a.Name, subscriptionRuleName));
            if (subscriptionRule == null)
            {
                await this.subscriptionClient.AddRuleAsync(new RuleDescription
                {
                    Filter = new CorrelationFilter
                    {
                        To = receiver
                    },
                    Name = subscriptionRuleName
                });
            }

            var ammendedRules = (await subscriptionClient.GetRulesAsync()).ToList();
            this.logger.LogInformation("Rule for {SubscriptionName} is {Name}", subscriptionClient.SubscriptionName, ammendedRules[0].Name);
        }

        private void RegisterOnMessageHandlerAndReceiveMessages()
        {
            // Configure the message handler options in terms of exception handling, number of concurrent messages to deliver, etc.
            var messageHandlerOptions = new MessageHandlerOptions(this.ExceptionReceivedHandler)
            {
                // Maximum number of concurrent calls to the callback ProcessMessagesAsync(), set to 1 for simplicity.
                // Set it according to how many messages the application wants to process in parallel.
                MaxConcurrentCalls = 1,

                // Indicates whether the message pump should automatically complete the messages after returning from user callback.
                // False below indicates the complete operation is handled by the user callback as in ProcessMessagesAsync().
                AutoComplete = false
            };

            // Register the function that processes messages.
            this.subscriptionClient.RegisterMessageHandler(this.ProcessMessagesAsync, messageHandlerOptions);

            this.logger.LogInformation("The function that processes messages for the {TopicName} topic has been registered", this.topicName);
        }

        private async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            var sequenceNumber = message.SystemProperties.SequenceNumber;
            this.logger.LogInformation("Received message {SequenceNumber} from the {TopicName} topic", sequenceNumber, this.topicName);

            var handlername = this.messageHandler.GetType().Name;
            this.logger.LogInformation("Executing handler {HandlerName}", handlername);
            await this.messageHandler.Execute(message.ReplyTo, message.Body);

            // Complete the message so that it is not received again.
            // This can be done only if the subscriptionClient is created in ReceiveMode.PeekLock mode (which is the default).
            await this.subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
            this.logger.LogInformation("The message {SequenceNumber} from the {TopicName} topic has been completed", sequenceNumber, this.topicName);

            // Note: Use the cancellationToken passed as necessary to determine if the subscriptionClient has already been closed.
            // If subscriptionClient has already been closed, you can choose to not call CompleteAsync() or AbandonAsync() etc.
            // to avoid unnecessary exceptions.
        }

        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            this.logger.LogError("Message handler encountered an exception {Exception} in the {TopicName} topic", exceptionReceivedEventArgs.Exception, this.topicName);

            var context = exceptionReceivedEventArgs.ExceptionReceivedContext;

            this.logger.LogError("Exception context for troubleshooting:");
            this.logger.LogError("Endpoint: {Endpoint}", context.Endpoint);
            this.logger.LogError("Entity path: {EntityPath}", context.EntityPath);
            this.logger.LogError("Executing action: {Action}", context.Action);

            return Task.CompletedTask;
        }
    }
}