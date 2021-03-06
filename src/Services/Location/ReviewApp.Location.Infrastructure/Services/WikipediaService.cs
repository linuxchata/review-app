﻿using System;
using System.Net.Http;
using System.Threading.Tasks;

using Newtonsoft.Json;

using ReviewApp.Location.Core.Application;
using ReviewApp.Location.Core.Application.Wikipedia;

namespace ReviewApp.Location.Infrastructure.Services
{
    public sealed class WikipediaService : IWikipediaService
    {
        private readonly IApplicationSettings settings;

        public WikipediaService(IApplicationSettings settings)
        {
            this.settings = settings;
        }

        public async Task<string> GetPageContent()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(this.settings.WikipediaBaseUrl)
            };

            var response = await client.GetAsync(this.settings.WikipediaLocationsPageUrl);

            if (response.IsSuccessStatusCode)
            {
                var stringContent = await response.Content.ReadAsStringAsync();
                var result = HandleResponse(stringContent);

                return result.Query.Pages[0].Revisions[0].Content;
            }

            return string.Empty;
        }

        private static WikipediaResponse HandleResponse(string stringContent)
        {
            var response = JsonConvert.DeserializeObject<WikipediaResponse>(stringContent);

            return response;
        }
    }
}