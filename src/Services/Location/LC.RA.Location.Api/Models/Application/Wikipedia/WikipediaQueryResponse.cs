using System.Collections.Generic;

namespace LC.RA.Location.Api.Models.Application.Wikipedia
{
    public sealed class WikipediaQueryResponse
    {
        public List<WikipediaPageResponse> Pages { get; set; }
    }
}