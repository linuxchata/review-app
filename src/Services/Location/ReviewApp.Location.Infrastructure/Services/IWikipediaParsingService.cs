using System.Collections.Generic;

using ReviewApp.Location.Core.Application.Wikipedia;

namespace ReviewApp.Location.Infrastructure.Services
{
    public interface IWikipediaParsingService
    {
        SortedSet<WikiPageElement> ParsePage(string content);

        List<WikiTableRowBase> ParseTable(SortedSet<WikiPageElement> page);
    }
}