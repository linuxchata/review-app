using System.Collections.Generic;
using System.Text.RegularExpressions;
using LC.RA.Synchronization.Api.Infrastructure.Extensions;
using LC.RA.Synchronization.Api.Infrastructure.Services;
using LC.RA.Synchronization.Api.Models.Application.Wikipedia;

namespace LC.RA.Synchronization.Api.Infrastructure.Handlers.WikiTableHandlers
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