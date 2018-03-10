using System;
using LC.RA.Location.Api.Infrastructure.Converters;
using LC.RA.Location.Api.Infrastructure.Services;
using LC.ServiceBusAdapter.Abstractions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace LC.RA.Location.Api.Tests.Infrastructure.Services
{
    public class LocationServiceMessageHandlerTests
    {
        private readonly LocationServiceMessageHandler sut;

        private readonly Mock<ILocationService> locationServiceMock;

        private readonly Mock<ITopicSenderService> topicSenderService;

        private readonly Mock<ILocationsConverter> locationsConverterMock;

        public LocationServiceMessageHandlerTests()
        {
            this.locationServiceMock = new Mock<ILocationService>();
            this.topicSenderService = new Mock<ITopicSenderService>();
            this.locationsConverterMock = new Mock<ILocationsConverter>();
            var loggerMock = new Mock<ILogger<LocationServiceMessageHandler>>();

            this.sut = new LocationServiceMessageHandler(
                this.locationServiceMock.Object,
                this.topicSenderService.Object,
                this.locationsConverterMock.Object,
                loggerMock.Object);
        }

        [Fact]
        public async void Execute_WhenBodyContainsTrue_ShouldTriggerSynchronization_Test()
        {
            // Arrange
            // Act
            await this.sut.Execute("to", BitConverter.GetBytes(true));

            // Assert
            this.locationServiceMock.Verify(a => a.GetLocations(), Times.Once);
        }

        [Fact]
        public async void Execute_WhenBodyContainsFalse_ShouldNotTriggerSynchronization_Test()
        {
            // Arrange
            // Act
            await this.sut.Execute("to", BitConverter.GetBytes(false));

            // Assert
            this.locationServiceMock.Verify(a => a.GetLocations(), Times.Never);
        }
    }
}