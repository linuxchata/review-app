using System.Collections.Generic;
using LC.RA.Location.Core.Application.Wikipedia;
using LC.RA.Location.Infrastructure.Services;
using Microsoft.Extensions.Logging;

namespace LC.RA.Location.Infrastructure.Handlers.WikiPageHandlers
{
    public abstract class PageBaseHandler
    {
        protected readonly ILogger<WikipediaParsingService> Logger;

        private PageBaseHandler nextHandler;

        protected PageBaseHandler(ILogger<WikipediaParsingService> logger)
        {
            this.Logger = logger;
        }

        public void SetNextHandler(PageBaseHandler handler)
        {
            this.nextHandler = handler;
        }

        public void HandlerRequest(string content, SortedSet<WikiPageElement> elements)
        {
            this.HandlerRequestInternal(content, elements);

            this.nextHandler?.HandlerRequest(content, elements);
        }

        protected abstract void HandlerRequestInternal(string content, SortedSet<WikiPageElement> elements);
    }
}
