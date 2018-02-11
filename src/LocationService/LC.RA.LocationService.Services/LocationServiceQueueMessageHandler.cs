using System;
using System.Threading.Tasks;
using LC.RA.LocationService.Services.Contracts;
using LC.ServiceBusAdapter.Abstractions;
using Microsoft.Extensions.Logging;

namespace LC.RA.LocationService.Services
{
    public sealed class LocationServiceQueueMessageHandler : IQueueMessageHandler
    {
        private readonly ILocationSynchronizationService locationSynchronizationService;

        private readonly ILogger<LocationServiceQueueMessageHandler> logger;

        public LocationServiceQueueMessageHandler(
            ILocationSynchronizationService locationSynchronizationService,
            ILogger<LocationServiceQueueMessageHandler> logger)
        {
            this.locationSynchronizationService = locationSynchronizationService;
            this.logger = logger;
        }

        public Task Execute(byte[] messageBody)
        {
            var startLocationSynchronization = BitConverter.ToBoolean(messageBody, 0);
            if (startLocationSynchronization)
            {
                this.logger.LogInformation("Triggering location synchronization process");
                return Task.Run(() => this.locationSynchronizationService.Synchronize());
            }

            return Task.Run(() => { });
        }
    }
}