﻿using System.Collections.Generic;

namespace LC.RA.SynchronizationService.Api.Models.Application.Wikipedia
{
    public sealed class WikipediaPageResponse
    {
        public int Pageid { get; set; }

        public int Ns { get; set; }

        public string Title { get; set; }

        public List<WikipediaRevisionResponse> Revisions { get; set; }
    }
}