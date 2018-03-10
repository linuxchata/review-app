using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LC.RA.Location.Api.Infrastructure.Converters;
using LC.ServiceBusAdapter.Abstractions;
using Microsoft.Extensions.Logging;

namespace LC.RA.Location.Api.Infrastructure.Services
{
    public sealed class LocationServiceMessageHandler : IMessageHandler
    {
        private readonly ILocationService locationService;

        private readonly ITopicSenderService topicSenderService;

        private readonly ILocationsConverter locationsConverter;

        private readonly ILogger<LocationServiceMessageHandler> logger;

        public LocationServiceMessageHandler(
            ILocationService locationService,
            ITopicSenderService topicSenderService,
            ILocationsConverter locationsConverter,
            ILogger<LocationServiceMessageHandler> logger)
        {
            this.locationService = locationService;
            this.topicSenderService = topicSenderService;
            this.locationsConverter = locationsConverter;
            this.logger = logger;
        }

        public Task Execute(string replyTo, byte[] messageBody)
        {
            var start = BitConverter.ToBoolean(messageBody, 0);
            if (start)
            {
                this.logger.LogInformation("Triggering getting locations");
                return Task.Run(() => this.locationService.GetLocations())
                    .ContinueWith(r => this.SendResponce(r.Result, replyTo));
            }

            return Task.Run(() => { });
        }

        private async Task SendResponce(IEnumerable<Models.Domain.Location> locations, string replyTo)
        {
            var locationsArray = this.locationsConverter.Convert(locations);
            this.logger.LogInformation("Locations have been converter to protobuf byte array of {size} B", locationsArray.Length);

            await this.topicSenderService.SendMessageAsync(locationsArray, replyTo);
            this.logger.LogInformation("Locations have been send to the queue");
        }
    }
}