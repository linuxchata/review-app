using System;
using System.Net.Http;
using System.Threading.Tasks;
using LC.RA.WebApi.Core.Application;
using LC.RA.WebApi.Core.Application.Wikipedia;
using LC.RA.WebApi.Services.Contracts;
using Newtonsoft.Json;

namespace LC.RA.WebApi.Services.Synchronization
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
                var result = this.HandleResponse(stringContent);

                return result.Query.Pages[0].Revisions[0].Content;
            }

            return string.Empty;
        }

        private WikipediaResponse HandleResponse(string stringContent)
        {
            var response = JsonConvert.DeserializeObject<WikipediaResponse>(stringContent);

            return response;
        }
    }
}