using System.Collections.Generic;

namespace ReviewSystem.Core.Application.Wikipedia
{
    public sealed class WikipediaQueryResponse
    {
        public List<WikipediaPageResponse> Pages { get; set; }
    }
}