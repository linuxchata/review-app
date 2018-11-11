using System.Collections.Generic;

namespace ReviewApp.Location.Core.Application.Wikipedia
{
    public sealed class WikipediaQueryResponse
    {
        public List<WikipediaPageResponse> Pages { get; set; }
    }
}