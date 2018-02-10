﻿using System.Collections.Generic;
using System.Threading.Tasks;
using LC.RA.Synchronization.Core.Application.Wikipedia;
using LC.RA.Synchronization.Core.Domain;
using LC.RA.Synchronization.Services.Contracts;

namespace LC.RA.Synchronization.Services
{
    public sealed class LocationSynchronizationService : ILocationSynchronizationService
    {
        private readonly IWikipediaService wikipediaService;

        private readonly IWikipediaParsingService wikipediaParsingService;

        public LocationSynchronizationService(
            IWikipediaService wikipediaService,
            IWikipediaParsingService wikipediaParsingService)
        {
            this.wikipediaService = wikipediaService;
            this.wikipediaParsingService = wikipediaParsingService;
        }

        public async void Synchronize()
        {
            await this.GetSourceLocations();
        }

        private async Task<IEnumerable<Location>> GetSourceLocations()
        {
            var pageContent = await this.wikipediaService.GetPageContent();

            var parsedPageContent = this.wikipediaParsingService.ParsePage(pageContent);
            var parsedTable = this.wikipediaParsingService.ParseTable(parsedPageContent);

            var source = new List<Location>();
            foreach (var row in parsedTable)
            {
                if (row is WikiTableRow)
                {
                    var location = new Location(this.GetName(row), this.GetRegion(row));
                    source.Add(location);
                }
            }

            return source;
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