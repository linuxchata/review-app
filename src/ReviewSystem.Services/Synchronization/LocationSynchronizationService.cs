using System.Collections.Generic;
using ReviewSystem.Core;
using ReviewSystem.Core.Application.Wikipedia;
using ReviewSystem.DataAccess.Contracts;
using ReviewSystem.Services.Contracts;

namespace ReviewSystem.Services.Synchronization
{
    public sealed class LocationSynchronizationService : ILocationSynchronizationService
    {
        private readonly IWikipediaService wikipediaService;

        private readonly IWikipediaParsingService wikipediaParsingService;

        private readonly ILocationRepository locationRepository;

        public LocationSynchronizationService(
            IWikipediaService wikipediaService,
            IWikipediaParsingService wikipediaParsingService,
            ILocationRepository locationRepository)
        {
            this.locationRepository = locationRepository;
            this.wikipediaParsingService = wikipediaParsingService;
            this.wikipediaService = wikipediaService;
        }

        public async void Synchronize()
        {
            var pageContent = await this.wikipediaService.GetPageContent();

            var parsedPageContent = this.wikipediaParsingService.ParsePage(pageContent);
            var parsedTable = this.wikipediaParsingService.ParseTable(parsedPageContent);

            var locations = new List<Location>();
            foreach (var row in parsedTable)
            {
                if (row is WikiTableRow)
                {
                    var location = new Location
                    {
                        Name = this.GetName(row),
                        Region = this.GetRegion(row)
                    };
                    locations.Add(location);
                }
            }
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