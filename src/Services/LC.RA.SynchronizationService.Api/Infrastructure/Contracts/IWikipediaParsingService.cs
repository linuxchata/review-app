using System.Collections.Generic;
using LC.RA.SynchronizationService.Api.Model.Application.Wikipedia;

namespace LC.RA.SynchronizationService.Api.Infrastructure.Contracts
{
    public interface IWikipediaParsingService
    {
        SortedSet<WikiPageElement> ParsePage(string content);

        List<WikiTableRowBase> ParseTable(SortedSet<WikiPageElement> page);
    }
}