using System.Collections.Generic;
using System.Text.RegularExpressions;
using LC.RA.SynchronizationService.Api.Infrastructure.Extensions;
using LC.RA.SynchronizationService.Api.Infrastructure.Services;
using LC.RA.SynchronizationService.Api.Model.Application.Wikipedia;

namespace LC.RA.SynchronizationService.Api.Infrastructure.Handlers.WikiTableHandlers
{
    public sealed class TableRowHeadersHandler : TableBaseHandler
    {
        protected override void HandlerRequestInternal(ref string content, List<WikiTableRowBase> rows)
        {
            var collections = RegexExtension.GetMatches(content, RegexPattern.TableHeaderMatchPattern);

            var header = new WikiTableRowBaseHeader();

            foreach (Match collection in collections)
            {
                header.Content.Add(this.GetHeaderContent(collection.Value));
            }

            rows.Add(header);
        }

        private string GetHeaderContent(string @string)
        {
            var matches = RegexExtension.GetMatches(@string, RegexPattern.TableTextHeaderMatchPattern);
            var contentValue = matches[0].Value;

            RegexExtension.Replace(ref contentValue, RegexPattern.BracesReplacePattern);

            return contentValue;
        }
    }
}