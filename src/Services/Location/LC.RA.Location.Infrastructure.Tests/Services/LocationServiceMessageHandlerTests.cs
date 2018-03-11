﻿using System;
using LC.RA.Location.Infrastructure.Converters;
using LC.RA.Location.Infrastructure.Services;
using LC.ServiceBusAdapter.Abstractions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace LC.RA.Location.Infrastructure.Tests.Services
{
    public class LocationServiceMessageHandlerTests
    {
        private readonly LocationServiceMessageHandler sut;

        private readonly Mock<ILocationService> locationServiceMock;

        public LocationServiceMessageHandlerTests()
        {
            this.locationServiceMock = new Mock<ILocationService>();
            var topicSenderService = new Mock<ITopicSenderService>();
            var locationsConverterMock = new Mock<ILocationsConverter>();
            var loggerMock = new Mock<ILogger<LocationServiceMessageHandler>>();

            this.sut = new LocationServiceMessageHandler(
                this.locationServiceMock.Object,
                topicSenderService.Object,
                locationsConverterMock.Object,
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