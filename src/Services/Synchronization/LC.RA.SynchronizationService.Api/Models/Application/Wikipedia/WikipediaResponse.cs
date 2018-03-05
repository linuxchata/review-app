namespace LC.RA.SynchronizationService.Api.Models.Application.Wikipedia
{
    public sealed class WikipediaResponse
    {
        public bool Batchcomplete { get; set; }

        public WikipediaQueryResponse Query { get; set; }
    }
}
