using System.Collections.Generic;
using System.Threading.Tasks;
using LC.RA.WebApi.Core.Domain;
using LC.RA.WebApi.Services.Contracts;
using LC.RA.WebApi.Services.Synchronization;
using Moq;
using Xunit;

namespace LC.RA.WebApi.Services.Tests
{
    public sealed class LocationSynchronizationServiceTests
    {
        private readonly Mock<IWikipediaService> wikipediaServiceMock;

        private readonly Mock<ILocationService> locationServiceMock;

        public LocationSynchronizationServiceTests()
        {
            this.wikipediaServiceMock = new Mock<IWikipediaService>();
            this.wikipediaServiceMock
                .Setup(a => a.GetPageContent())
                .Returns(Task.FromResult(Properties.Resources.PageContent));

            this.locationServiceMock = new Mock<ILocationService>();
        }

        [Fact]
        public void Synchronize_WhenContentIsNotEmpty_ShouldNotThrowException_Test()
        {
            // Arrange
            var parsingService = new WikipediaParsingService();
            var sut = new LocationSynchronizationService(
                this.wikipediaServiceMock.Object,
                parsingService,
                this.locationServiceMock.Object);

            // Act
            sut.Synchronize(new List<Location>());

            // Assert
            this.wikipediaServiceMock.Verify(a => a.GetPageContent(), Times.Once);
            this.locationServiceMock.Verify(a => a.GetAllAsync(), Times.Once);
            this.locationServiceMock.Verify(a => a.CreateAsync(It.IsAny<Location>(), It.IsAny<string>()), Times.AtLeastOnce);
        }
    }
}