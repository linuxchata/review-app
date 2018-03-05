﻿using System.Collections.Generic;

namespace LC.RA.SynchronizationService.Api.Model.Application.Wikipedia
{
    public sealed class WikipediaQueryResponse
    {
        public List<WikipediaPageResponse> Pages { get; set; }
    }
}