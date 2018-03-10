using LC.RA.Synchronization.Api.Controllers;
using LC.RA.Synchronization.Api.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace LC.RA.Synchronization.Api.Tests.Controllers
{
    public class SyncControllerTests
    {
        private readonly SyncController sut;

        private readonly Mock<ILocationService> locationServiceMock;

        public SyncControllerTests()
        {
            this.locationServiceMock = new Mock<ILocationService>();

            this.sut = new SyncController(this.locationServiceMock.Object);
        }

        [Fact]
        public void Synchronize_ShouldReturnOkResult_Test()
        {
            // Arrange
            // Act
            var response = this.sut.SyncLocations();

            // Assert
            var result = Assert.IsType<OkResult>(response);
            Assert.NotNull(result);
            this.locationServiceMock.Verify(a => a.GetLocations(), Times.Once);
        }
    }
}