﻿using System.Collections.Generic;
using System.Text.RegularExpressions;
using LC.RA.Location.Api.Infrastructure.Extensions;
using LC.RA.Location.Api.Models.Application.Wikipedia;

namespace LC.RA.Location.Api.Infrastructure.Handlers.WikiPageHandlers
{
    public sealed class PageTablesHandler : PageBaseHandler
    {
        protected override void HandlerRequestInternal(string content, SortedSet<WikiPageElement> elements)
        {
            var tablePattern = @"\{\|[\s\S]+?\|\}";
            var collection = RegexExtension.GetMatches(content, tablePattern);

            foreach (Group group in collection[0].Groups)
            {
                elements.Add(new WikiPageElement
                {
                    StartIndex = group.Index,
                    Length = group.Length,
                    Content = group.Value,
                    ContentType = WikiPageContentType.Table
                });
            }
        }
    }
}