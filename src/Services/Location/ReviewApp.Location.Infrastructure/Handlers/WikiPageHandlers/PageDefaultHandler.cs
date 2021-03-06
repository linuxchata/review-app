﻿using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.Logging;

using ReviewApp.Location.Core.Application.Wikipedia;
using ReviewApp.Location.Infrastructure.Services;

namespace ReviewApp.Location.Infrastructure.Handlers.WikiPageHandlers
{
    public sealed class PageDefaultHandler : PageBaseHandler
    {
        public PageDefaultHandler(ILogger<WikipediaParsingService> logger) : base(logger)
        {
        }

        protected override void HandlerRequestInternal(string content, SortedSet<WikiPageElement> elements)
        {
            var missingPageElements = new List<WikiPageElement>();

            var i = 0;
            foreach (var element in elements)
            {
                var j = element.StartIndex;
                var delta = j - i;
                if (delta > 0)
                {
                    var length = delta;
                    missingPageElements.Add(new WikiPageElement
                    {
                        StartIndex = i,
                        Length = length,
                        Content = content.Substring(i, length),
                        ContentType = WikiPageContentType.Paragraph
                    });
                    i = element.EndIndex;
                }
            }

            // Make sure that content after last known page element is added
            var lastElement = elements.LastOrDefault();
            if (lastElement.EndIndex < content.Length)
            {
                var start = lastElement.EndIndex;
                var length = content.Length - start;
                missingPageElements.Add(new WikiPageElement
                {
                    StartIndex = start,
                    Length = length,
                    Content = content.Substring(i, length),
                    ContentType = WikiPageContentType.Paragraph
                });
            }

            foreach (var element in missingPageElements)
            {
                elements.Add(element);
            }
        }
    }
}