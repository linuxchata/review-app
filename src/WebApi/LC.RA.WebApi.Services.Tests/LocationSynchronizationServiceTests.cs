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
        private readonly Mock<ILocationService> locationServiceMock;

        public LocationSynchronizationServiceTests()
        {
            this.locationServiceMock = new Mock<ILocationService>();
        }

        [Fact]
        public void Synchronize_WhenContentIsNotEmpty_ShouldNotThrowException_Test()
        {
            // Arrange
            IEnumerable<Location> locations = new List<Location>
            {
                new Location("Kiev", "Kiev")
            };
            var sut = new LocationSynchronizationService(this.locationServiceMock.Object);

            // Act
            sut.Synchronize(locations);

            // Assert
            this.locationServiceMock.Verify(a => a.GetAllAsync(), Times.Once);
            this.locationServiceMock.Verify(a => a.CreateAsync(It.IsAny<Location>(), It.IsAny<string>()), Times.AtLeastOnce);
        }
    }
}