using System;
using System.Threading;
using System.Threading.Tasks;
using LC.ServiceBusAdapter.Abstractions;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;

namespace LC.ServiceBusAdapter
{
    public sealed class QueueMessageReceiverService : IQueueMessageReceiverService
    {
        private readonly string connectionString;

        private readonly string queueName;

        private readonly IQueueMessageHandler messageHandler;

        private readonly ILogger logger;

        private IQueueClient queueClient;

        public QueueMessageReceiverService(
            string connectionString,
            string queueName,
            IQueueMessageHandler messageHandler,
            ILogger<QueueMessageReceiverService> logger)
        {
            this.connectionString = connectionString;
            this.queueName = queueName;
            this.messageHandler = messageHandler;
            this.logger = logger;
        }

        public async Task ListenForMessages(CancellationToken cancellationToken)
        {
            try
            {
                queueClient = new QueueClient(this.connectionString, this.queueName);
                this.logger.LogInformation("Queue client for {QueueName} queue has been created", this.queueName);

                RegisterOnMessageHandlerAndReceiveMessages();

                await Task.Delay(-1, cancellationToken);

                await queueClient.CloseAsync();
            }
            catch (Exception exception)
            {
                this.logger.LogError("Error in queue client for {QueueName} queue: {Exception}", this.queueName, exception);
                throw;
            }
        }

        private void RegisterOnMessageHandlerAndReceiveMessages()
        {
            // Configure the message handler options in terms of exception handling, number of concurrent messages to deliver, etc.
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                // Maximum number of concurrent calls to the callback ProcessMessagesAsync(), set to 1 for simplicity.
                // Set it according to how many messages the application wants to process in parallel.
                MaxConcurrentCalls = 1,

                // Indicates whether the message pump should automatically complete the messages after returning from user callback.
                // False below indicates the complete operation is handled by the user callback as in ProcessMessagesAsync().
                AutoComplete = false
            };

            queueClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);

            this.logger.LogInformation("The function that processes messages for {QueueName} queue has been registered", this.queueName);
        }

        private async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            var sequenceNumber = message.SystemProperties.SequenceNumber;
            this.logger.LogInformation("Received message {SequenceNumber} from {QueueName} queue", sequenceNumber, this.queueName);

            var handlername = this.messageHandler.GetType().Name;
            this.logger.LogInformation("Executing handler {HandlerName}", handlername);
            await messageHandler.Execute(message.Body);

            await queueClient.CompleteAsync(message.SystemProperties.LockToken);
            this.logger.LogInformation("The message {SequenceNumber} from {QueueName} queue has been completed", sequenceNumber, this.queueName);

            // Note: Use the cancellationToken passed as necessary to determine if the queueClient has already been closed.
            // If queueClient has already been closed, you can choose to not call CompleteAsync() or AbandonAsync() etc.
            // to avoid unnecessary exceptions.
        }

        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            this.logger.LogError("Message handler encountered an exception {Exception} in the {QueueName} queue", exceptionReceivedEventArgs.Exception, this.queueName);

            var context = exceptionReceivedEventArgs.ExceptionReceivedContext;

            this.logger.LogError("Exception context for troubleshooting:");
            this.logger.LogError("Endpoint: {Endpoint}", context.Endpoint);
            this.logger.LogError("Entity path: {EntityPath}", context.EntityPath);
            this.logger.LogError("Executing action: {Action}", context.Action);

            return Task.CompletedTask;
        }
    }
}