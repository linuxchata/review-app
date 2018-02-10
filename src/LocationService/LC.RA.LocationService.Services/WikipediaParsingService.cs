﻿using System.Collections.Generic;
using System.Linq;
using LC.RA.LocationService.Core.Application.Wikipedia;
using LC.RA.LocationService.Services.Contracts;
using LC.RA.LocationService.Services.WikiPageHandlers;
using LC.RA.LocationService.Services.WikiTableHandlers;

namespace LC.RA.LocationService.Services
{
    public sealed class WikipediaParsingService : IWikipediaParsingService
    {
        public SortedSet<WikiPageElement> ParsePage(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return new SortedSet<WikiPageElement>();
            }

            // Parse tables
            var tablesHandler = new PageTablesHandler();

            // Parse page headers
            var headersHandler = new PageHeadersHandler();

            // Include rest of the page
            var defaultHandler = new PageDefaultHandler();

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