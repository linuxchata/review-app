using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LC.RA.TransferObjects;
using LC.RA.WebApi.Services.Contracts;
using LC.ServiceBusAdapter.Abstractions;
using Microsoft.Extensions.Logging;

namespace LC.RA.WebApi.Services
{
    public sealed class WebApiQueueMessageHandler : IQueueMessageHandler
    {
        private readonly ILocationConverter locationConverter;

        private readonly ILocationSynchronizationService locationSynchronizationService;

        private readonly ILogger<WebApiQueueMessageHandler> logger;

        public WebApiQueueMessageHandler(
            ILocationConverter locationConverter,
            ILocationSynchronizationService locationSynchronizationService,
            ILogger<WebApiQueueMessageHandler> logger)
        {
            this.locationConverter = locationConverter;
            this.locationSynchronizationService = locationSynchronizationService;
            this.logger = logger;
        }

        public Task Execute(byte[] messageBody)
        {
            var locationsToConvert = Utilities.Extensions.FormatterExtension.Deserialize<List<Location>>(messageBody);
            var locations = locationsToConvert.Select(a => this.locationConverter.Convert(a));

            return Task.Run(() => { this.locationSynchronizationService.Synchronize(locations); });
        }
    }
}