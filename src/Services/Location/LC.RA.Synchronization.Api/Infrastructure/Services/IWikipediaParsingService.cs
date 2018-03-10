using System.Collections.Generic;
using LC.RA.Location.Api.Models.Application.Wikipedia;

namespace LC.RA.Location.Api.Infrastructure.Services
{
    public interface IWikipediaParsingService
    {
        SortedSet<WikiPageElement> ParsePage(string content);

        List<WikiTableRowBase> ParseTable(SortedSet<WikiPageElement> page);
    }
}