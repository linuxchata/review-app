using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ReviewSystem.Core.Application.Wikipedia;
using ReviewSystem.Services.Contracts;

namespace ReviewSystem.Services.Synchronization
{
    public sealed class WikipediaService : IWikipediaService
    {
        public async Task<WikipediaResponse> GetPageContent()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(@"https://uk.wikipedia.org/")
            };

            var url = @"w/api.php?action=query&titles=%D0%9C%D1%96%D1%81%D1%82%D0%B0_%D0%A3%D0%BA%D1%80%D0%B0%D1%97%D0%BD%D0%B8_(%D0%B7%D0%B0_%D0%BD%D0%B0%D1%81%D0%B5%D0%BB%D0%B5%D0%BD%D0%BD%D1%8F%D0%BC)&prop=revisions&rvprop=content&format=json&formatversion=2";
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var stringContent = await response.Content.ReadAsStringAsync();
                return this.HandleResponse(stringContent);
            }

            return new WikipediaResponse();
        }

        private WikipediaResponse HandleResponse(string stringContent)
        {
            var response = JsonConvert.DeserializeObject<WikipediaResponse>(stringContent);

            return response;
        }
    }
}