using System.Collections.Generic;
using LC.RA.LocationService.Core.Application.Wikipedia;

namespace LC.RA.LocationService.Services.Contracts
{
    public interface IWikipediaParsingService
    {
        SortedSet<WikiPageElement> ParsePage(string content);

        List<WikiTableRowBase> ParseTable(SortedSet<WikiPageElement> page);
    }
}