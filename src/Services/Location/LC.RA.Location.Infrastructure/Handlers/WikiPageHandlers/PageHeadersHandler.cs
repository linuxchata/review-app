using System.Collections.Generic;
using System.Text.RegularExpressions;
using LC.RA.Location.Core.Application.Wikipedia;
using LC.RA.Location.Infrastructure.Extensions;
using LC.RA.Location.Infrastructure.Services;
using Microsoft.Extensions.Logging;

namespace LC.RA.Location.Infrastructure.Handlers.WikiPageHandlers
{
    public sealed class PageHeadersHandler : PageBaseHandler
    {
        public PageHeadersHandler(ILogger<WikipediaParsingService> logger) : base(logger)
        {
        }

        protected override void HandlerRequestInternal(string content, SortedSet<WikiPageElement> elements)
        {
            var headerPattern = @"(={1,5}[^=]{1,200}?={1,5})";
            var collection = RegexExtension.GetMatches(content, headerPattern);

            if (collection.Count == 0)
            {
                this.Logger.LogDebug("Headers content was not found");
                return;
            }

            foreach (Group group in collection[0].Groups)
            {
                elements.Add(new WikiPageElement
                {
                    StartIndex = group.Index,
                    Length = group.Length,
                    Content = group.Value,
                    ContentType = WikiPageContentType.Header
                });
            }
        }
    }
}