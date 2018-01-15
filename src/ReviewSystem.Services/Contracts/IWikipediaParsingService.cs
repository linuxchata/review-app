using System.Collections.Generic;
using ReviewSystem.Core.Application.Wikipedia;

namespace ReviewSystem.Services.Contracts
{
    public interface IWikipediaParsingService
    {
        SortedSet<WikiPageElement> ParsePage(string content);

        List<WikiTableRowBase> ParseTable(SortedSet<WikiPageElement> page);
    }
}