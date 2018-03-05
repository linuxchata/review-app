using System.Collections.Generic;
using System.Text.RegularExpressions;
using LC.RA.SynchronizationService.Api.Infrastructure.Extensions;
using LC.RA.SynchronizationService.Api.Models.Application.Wikipedia;

namespace LC.RA.SynchronizationService.Api.Infrastructure.Handlers.WikiPageHandlers
{
    public sealed class PageHeadersHandler : PageBaseHandler
    {
        protected override void HandlerRequestInternal(string content, SortedSet<WikiPageElement> elements)
        {
            var headerPattern = @"(={1,5}[^=]{1,200}?={1,5})";
            var collection = RegexExtension.GetMatches(content, headerPattern);

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