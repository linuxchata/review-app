﻿using System;
using System.Threading.Tasks;
using LC.ServiceBusAdapter.Abstractions;
using Microsoft.Extensions.Logging;

namespace LC.RA.SynchronizationService.Api.Infrastructure.Services
{
    public sealed class LocationServiceQueueMessageHandler : IQueueMessageHandler
    {
        private readonly ILocationService locationService;

        private readonly ILogger<LocationServiceQueueMessageHandler> logger;

        public LocationServiceQueueMessageHandler(
            ILocationService locationService,
            ILogger<LocationServiceQueueMessageHandler> logger)
        {
            this.locationService = locationService;
            this.logger = logger;
        }

        public Task Execute(byte[] messageBody)
        {
            var start = BitConverter.ToBoolean(messageBody, 0);
            if (start)
            {
                this.logger.LogInformation("Triggering getting locations");
                return Task.Run(() => this.locationService.Synchronize());
            }

            return Task.Run(() => { });
        }
    }
}