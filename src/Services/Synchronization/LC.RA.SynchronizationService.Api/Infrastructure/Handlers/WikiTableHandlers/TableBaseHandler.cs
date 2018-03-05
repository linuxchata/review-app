using System.Collections.Generic;
using LC.RA.Synchronization.Api.Models.Application.Wikipedia;

namespace LC.RA.Synchronization.Api.Infrastructure.Handlers.WikiTableHandlers
{
    public abstract class TableBaseHandler
    {
        private TableBaseHandler nextHandler;

        public void SetNextHandler(TableBaseHandler handler)
        {
            this.nextHandler = handler;
        }

        public void HandlerRequest(ref string content, List<WikiTableRowBase> rows)
        {
            this.HandlerRequestInternal(ref content, rows);

            this.nextHandler?.HandlerRequest(ref content, rows);
        }

        protected abstract void HandlerRequestInternal(ref string content, List<WikiTableRowBase> rows);
    }
}
