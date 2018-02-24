using System.Threading.Tasks;
using LC.RA.WebApi.Services.Contracts;
using LC.ServiceBusAdapter.Abstractions;
using Microsoft.Extensions.Logging;

namespace LC.RA.WebApi.Services
{
    public sealed class WebApiQueueMessageHandler : IQueueMessageHandler
    {
        private readonly ILocationsConverter locationConverter;

        private readonly ILocationService locationService;

        private readonly ILogger<WebApiQueueMessageHandler> logger;

        public WebApiQueueMessageHandler(
            ILocationsConverter locationConverter,
            ILocationService locationService,
            ILogger<WebApiQueueMessageHandler> logger)
        {
            this.locationConverter = locationConverter;
            this.locationService = locationService;
            this.logger = logger;
        }

        public Task Execute(byte[] messageBody)
        {
            this.logger.LogInformation("Message of {Length} B have been received from the message queue", messageBody.Length);

            var locations = this.locationConverter.Convert(messageBody);

            this.logger.LogInformation("Locations have been converted to domain objects");

            return Task.Run(() => { this.locationService.Synchronize(locations); });
        }
    }
}