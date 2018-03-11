using System.Collections.Generic;
using LC.RA.Location.Core.Application.Wikipedia;

namespace LC.RA.Location.Infrastructure.Handlers.WikiPageHandlers
{
    public abstract class PageBaseHandler
    {
        private PageBaseHandler nextHandler;

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
