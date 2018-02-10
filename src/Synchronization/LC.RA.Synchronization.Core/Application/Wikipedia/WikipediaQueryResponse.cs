using System.Collections.Generic;

namespace LC.RA.Synchronization.Core.Application.Wikipedia
{
    public sealed class WikipediaQueryResponse
    {
        public List<WikipediaPageResponse> Pages { get; set; }
    }
}