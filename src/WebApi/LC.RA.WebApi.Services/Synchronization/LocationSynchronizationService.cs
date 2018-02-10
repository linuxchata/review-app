using System.Collections.Generic;
using System.Threading.Tasks;
using LC.RA.WebApi.Core.Application.Wikipedia;
using LC.RA.WebApi.Core.Domain;
using LC.RA.WebApi.Services.Contracts;

namespace LC.RA.WebApi.Services.Synchronization
{
    public sealed class LocationSynchronizationService : ILocationSynchronizationService
    {
        private readonly IWikipediaService wikipediaService;

        private readonly IWikipediaParsingService wikipediaParsingService;

        private readonly ILocationService locationService;

        private readonly IServiceBusService serviceBusService;

        public LocationSynchronizationService(
            IWikipediaService wikipediaService,
            IWikipediaParsingService wikipediaParsingService,
            ILocationService locationService,
            IServiceBusService serviceBusService)
        {
            this.wikipediaService = wikipediaService;
            this.wikipediaParsingService = wikipediaParsingService;
            this.locationService = locationService;
            this.serviceBusService = serviceBusService;
        }

        public async void Synchronize()
        {
            await this.serviceBusService.SendMessage();

            var source = await this.GetSourceLocations();
            var existed = await this.GetExistedLocations();

            foreach (var location in source)
            {
                if (!existed.Contains(location))
                {
                    await this.locationService.CreateAsync(location, "Synchronization User");
                }
            }
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

        private async Task<HashSet<Location>> GetExistedLocations()
        {
            return new HashSet<Location>(await this.locationService.GetAllAsync());
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