using System.Collections.Generic;

using ReviewApp.Location.Core.Application.Wikipedia;
using ReviewApp.Location.Infrastructure.Extensions;
using ReviewApp.Location.Infrastructure.Services;

namespace ReviewApp.Location.Infrastructure.Handlers.WikiTableHandlers
{
    public sealed class TablePreHandler : TableBaseHandler
    {
        protected override void HandlerRequestInternal(ref string content, List<WikiTableRowBase> columns)
        {
            RegexExtension.Replace(ref content, RegexPattern.TableRowSpanMatchPattern);
        }
    }
}