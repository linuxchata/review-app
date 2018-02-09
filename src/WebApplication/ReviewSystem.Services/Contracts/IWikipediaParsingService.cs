using System.Collections.Generic;
using LC.RA.WebApi.Core.Application.Wikipedia;

namespace LC.RA.WebApi.Services.Contracts
{
    public interface IWikipediaParsingService
    {
        SortedSet<WikiPageElement> ParsePage(string content);

        List<WikiTableRowBase> ParseTable(SortedSet<WikiPageElement> page);
    }
}