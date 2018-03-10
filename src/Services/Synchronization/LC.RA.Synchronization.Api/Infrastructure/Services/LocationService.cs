﻿using System.Collections.Generic;
using System.Threading.Tasks;
using LC.RA.Synchronization.Api.Infrastructure.Extensions;
using LC.RA.Synchronization.Api.Models.Application.Wikipedia;
using LC.RA.Synchronization.Api.Models.Domain;
using Microsoft.Extensions.Logging;

namespace LC.RA.Synchronization.Api.Infrastructure.Services
{
    public sealed class LocationService : ILocationService
    {
        private readonly IWikipediaService wikipediaService;

        private readonly IWikipediaParsingService wikipediaParsingService;

        private readonly ILogger<LocationService> logger;

        public LocationService(
            IWikipediaService wikipediaService,
            IWikipediaParsingService wikipediaParsingService,
            ILogger<LocationService> logger)
        {
            this.wikipediaService = wikipediaService;
            this.wikipediaParsingService = wikipediaParsingService;
            this.logger = logger;
        }

        public async Task<IEnumerable<Location>> GetLocations()
        {
            var locations = await this.GetSourceLocations();
            return locations;
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