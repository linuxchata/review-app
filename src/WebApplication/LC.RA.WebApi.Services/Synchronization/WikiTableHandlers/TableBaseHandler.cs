﻿using System.Collections.Generic;
using LC.RA.WebApi.Core.Application.Wikipedia;

namespace LC.RA.WebApi.Services.Synchronization.WikiTableHandlers
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