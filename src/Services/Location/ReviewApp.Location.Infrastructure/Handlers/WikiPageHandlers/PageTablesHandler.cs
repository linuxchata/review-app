using System.Collections.Generic;
using System.Text.RegularExpressions;

using Microsoft.Extensions.Logging;

using ReviewApp.Location.Core.Application.Wikipedia;
using ReviewApp.Location.Infrastructure.Extensions;
using ReviewApp.Location.Infrastructure.Services;

namespace ReviewApp.Location.Infrastructure.Handlers.WikiPageHandlers
{
    public sealed class PageTablesHandler : PageBaseHandler
    {
        public PageTablesHandler(ILogger<WikipediaParsingService> logger) : base(logger)
        {
        }

        protected override void HandlerRequestInternal(string content, SortedSet<WikiPageElement> elements)
        {
            var tablePattern = @"\{\|[\s\S]+?\|\}";
            var collection = RegexExtension.GetMatches(content, tablePattern);

            if (collection.Count == 0)
            {
                this.Logger.LogDebug("Table content was not found");
                return;
            }

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