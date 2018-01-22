using System.Threading.Tasks;
using Moq;
using ReviewSystem.Services.Contracts;
using ReviewSystem.Services.Synchronization;
using Xunit;

namespace ReviewSystem.Services.Tests
{
    public sealed class LocationSynchronizationServiceTests
    {
        private readonly Mock<IWikipediaService> wikipediaServiceMock;

        public LocationSynchronizationServiceTests()
        {
            this.wikipediaServiceMock = new Mock<IWikipediaService>();
            this.wikipediaServiceMock
                .Setup(a => a.GetPageContent())
                .Returns(Task.FromResult(string.Empty));
        }

        [Fact]
        public void Synchronize_WhenContentIsNotEmpty_ShouldNotThrowException_Test()
        {
            // Arrange
            var parsingService = new WikipediaParsingService();
            var locationService = new LocationService(null);
            var sut = new LocationSynchronizationService(
                this.wikipediaServiceMock.Object,
                parsingService,
                locationService);

            // Act
            sut.Synchronize();

            // Assert
        }
    }
}