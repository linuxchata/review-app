using System.Collections.Generic;
using ReviewSystem.Core.Application.Wikipedia;

namespace ReviewSystem.Services.Synchronization.WikiTableHandlers
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
