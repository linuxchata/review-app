using System.Collections.Generic;
using LC.RA.Synchronization.Api.Models.Application.Wikipedia;

namespace LC.RA.Synchronization.Api.Infrastructure.Services
{
    public interface IWikipediaParsingService
    {
        SortedSet<WikiPageElement> ParsePage(string content);

        List<WikiTableRowBase> ParseTable(SortedSet<WikiPageElement> page);
    }
}