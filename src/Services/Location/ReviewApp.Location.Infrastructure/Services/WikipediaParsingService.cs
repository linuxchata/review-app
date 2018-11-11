using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.Logging;

using ReviewApp.Location.Core.Application.Wikipedia;
using ReviewApp.Location.Infrastructure.Handlers.WikiPageHandlers;
using ReviewApp.Location.Infrastructure.Handlers.WikiTableHandlers;

namespace ReviewApp.Location.Infrastructure.Services
{
    public sealed class WikipediaParsingService : IWikipediaParsingService
    {
        private readonly ILogger<WikipediaParsingService> logger;

        public WikipediaParsingService(ILogger<WikipediaParsingService> logger)
        {
            this.logger = logger;
        }

        public SortedSet<WikiPageElement> ParsePage(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return new SortedSet<WikiPageElement>();
            }

            // Parse tables
            var tablesHandler = new PageTablesHandler(this.logger);

            // Parse page headers
            var headersHandler = new PageHeadersHandler(this.logger);

            // Include rest of the page
            var defaultHandler = new PageDefaultHandler(this.logger);

            tablesHandler.SetNextHandler(headersHandler);
            headersHandler.SetNextHandler(defaultHandler);

            var pageElements = new SortedSet<WikiPageElement>(new ComparerByStartIndex());
            tablesHandler.HandlerRequest(content, pageElements);

            return pageElements;
        }

        public List<WikiTableRowBase> ParseTable(SortedSet<WikiPageElement> page)
        {
            if (page == null)
            {
                return new List<WikiTableRowBase>();
            }

            var tableContent = page.FirstOrDefault(a => a.ContentType == WikiPageContentType.Table);
            if (tableContent == null)
            {
                return new List<WikiTableRowBase>();
            }

            // Prepare a table for parsing
            var preHandler = new TablePreHandler();

            // Parse header of a table
            var headersHandler = new TableRowHeadersHandler();

            // Parse rows of a table
            var rowsHandler = new TableRowsHandler();

            preHandler.SetNextHandler(headersHandler);
            headersHandler.SetNextHandler(rowsHandler);

            var content = tableContent.Content;
            var tableElements = new List<WikiTableRowBase>();
            preHandler.HandlerRequest(ref content, tableElements);

            return tableElements;
        }
    }
}