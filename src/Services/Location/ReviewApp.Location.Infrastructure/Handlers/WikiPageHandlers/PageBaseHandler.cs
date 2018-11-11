using System.Collections.Generic;

using Microsoft.Extensions.Logging;

using ReviewApp.Location.Core.Application.Wikipedia;
using ReviewApp.Location.Infrastructure.Services;

namespace ReviewApp.Location.Infrastructure.Handlers.WikiPageHandlers
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
