using System.Collections.Generic;
using System.Threading.Tasks;
using LC.RA.Synchronization.Api.Infrastructure.Converters;
using LC.RA.Synchronization.Api.Infrastructure.Extensions;
using LC.RA.Synchronization.Api.Models.Application.Wikipedia;
using LC.RA.Synchronization.Api.Models.Domain;
using LC.ServiceBusAdapter.Abstractions;
using Microsoft.Extensions.Logging;

namespace LC.RA.Synchronization.Api.Infrastructure.Services
{
    public sealed class LocationService : ILocationService
    {
        private readonly IWikipediaService wikipediaService;

        private readonly IWikipediaParsingService wikipediaParsingService;

        private readonly IQueueMessageSenderService queueMessageSenderService;

        private readonly ILocationsConverter locationsConverter;

        private readonly ILogger<LocationService> logger;

        public LocationService(
            IWikipediaService wikipediaService,
            IWikipediaParsingService wikipediaParsingService,
            IQueueMessageSenderService queueMessageSenderService,
            ILocationsConverter locationsConverter,
            ILogger<LocationService> logger)
        {
            this.wikipediaService = wikipediaService;
            this.wikipediaParsingService = wikipediaParsingService;
            this.queueMessageSenderService = queueMessageSenderService;
            this.logger = logger;
            this.locationsConverter = locationsConverter;
        }

        public async void Synchronize()
        {
            var locations = await this.GetSourceLocations();

            var locationsArray = this.locationsConverter.Convert(locations);

            this.logger.LogInformation("Locations have been converter to protobuf byte array of {size} B", locationsArray.Length);

            await this.queueMessageSenderService.SendMessage(locationsArray);

            this.logger.LogInformation("Locations have been send to the queue");
        }

        private async Task<IEnumerable<Location>> GetSourceLocations()
        {
            var pageContent = await this.wikipediaService.GetPageContent();

            var parsedPageContent = this.wikipediaParsingService.ParsePage(pageContent);
            var parsedTable = this.wikipediaParsingService.ParseTable(parsedPageContent);

            var locations = new List<Location>();
            foreach (var row in parsedTable)
            {
                if (row is WikiTableRow)
                {
                    var location = new Location(this.GetName(row), this.GetRegion(row));
                    locations.Add(location);
                }
            }

            this.logger.LogInformation("{Count} locations have been found", locations.Count);

            return locations;
        }

        private string GetName(WikiTableRowBase row)
        {
            var nameMatches = RegexExtension.GetMatches(row.Content[0], RegexPattern.LocationNameMatchPattern);
            var name = nameMatches[0].Groups[1].Value;

            var nameCorrectedMatches = RegexExtension.GetMatches(name, RegexPattern.LocationNameCorretionMatchPattern);
            if (nameCorrectedMatches.Count > 0)
            {
                var nameCorrected = nameCorrectedMatches[0].Groups[1].Value;
                name = nameCorrected;
            }

            return name;
        }

        private string GetRegion(WikiTableRowBase row)
        {
            var regionMatches = RegexExtension.GetMatches(row.Content[1], RegexPattern.LocationRegionMatchPattern);
            var region = regionMatches[0].Groups[1].Value;
            return region;
        }
    }
}