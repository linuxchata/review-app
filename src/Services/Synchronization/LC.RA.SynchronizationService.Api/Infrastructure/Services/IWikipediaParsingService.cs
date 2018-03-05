using System.Collections.Generic;
using LC.RA.SynchronizationService.Api.Models.Application.Wikipedia;

namespace LC.RA.SynchronizationService.Api.Infrastructure.Services
{
    public interface IWikipediaParsingService
    {
        SortedSet<WikiPageElement> ParsePage(string content);

        List<WikiTableRowBase> ParseTable(SortedSet<WikiPageElement> page);
    }
}