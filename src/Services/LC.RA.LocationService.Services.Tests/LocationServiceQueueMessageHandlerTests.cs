using System;
using LC.RA.LocationService.Services.Contracts;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace LC.RA.LocationService.Services.Tests
{
    public class LocationServiceQueueMessageHandlerTests
    {
        private readonly LocationServiceQueueMessageHandler sut;

        private readonly Mock<ILocationService> locationServiceMock;

        private readonly Mock<ILogger<LocationServiceQueueMessageHandler>> loggerMock;

        public LocationServiceQueueMessageHandlerTests()
        {
            this.locationServiceMock = new Mock<ILocationService>();
            this.loggerMock = new Mock<ILogger<LocationServiceQueueMessageHandler>>();

            this.sut = new LocationServiceQueueMessageHandler(
                this.locationServiceMock.Object,
                this.loggerMock.Object);
        }

        [Fact]
        public async void Execute_WhenBodyContainsTrue_ShouldTriggerSynchronization_Test()
        {
            // Arrange
            // Act
            await this.sut.Execute(BitConverter.GetBytes(true));

            // Assert
            this.locationServiceMock.Verify(a => a.Synchronize(), Times.Once);
        }

        [Fact]
        public async void Execute_WhenBodyContainsFalse_ShouldNotTriggerSynchronization_Test()
        {
            // Arrange
            // Act
            await this.sut.Execute(BitConverter.GetBytes(false));

            // Assert
            this.locationServiceMock.Verify(a => a.Synchronize(), Times.Never);
        }
    }
}