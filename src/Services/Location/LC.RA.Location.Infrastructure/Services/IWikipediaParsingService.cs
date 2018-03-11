using System.Collections.Generic;
using LC.RA.Location.Core.Application.Wikipedia;

namespace LC.RA.Location.Infrastructure.Services
{
    public interface IWikipediaParsingService
    {
        SortedSet<WikiPageElement> ParsePage(string content);

        List<WikiTableRowBase> ParseTable(SortedSet<WikiPageElement> page);
    }
}