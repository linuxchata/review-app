using ReviewSystem.Services.Synchronization;
using Xunit;

namespace ReviewSystem.Services.Tests
{
    public class LocationSynchronizationServiceTests
    {
        [Fact]
        public void Synchronize_WhenContentIsNotEmpty_ShouldNotThrowException_Test()
        {
            var wiki = new WikipediaService();
            var parser = new WikipediaParsingService();
            var service = new LocationSynchronizationService(wiki, parser, null);
            service.Synchronize();
        }
    }
}