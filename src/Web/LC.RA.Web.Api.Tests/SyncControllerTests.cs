using LC.RA.Web.Api.Controllers;
using LC.RA.Web.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace LC.RA.Web.Api.Tests
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
        public async void SyncLocations_ShouldReturnOkResult_Test()
        {
            // Arrange
            // Act
            var response = await this.sut.SyncLocations();

            // Assert
            var result = Assert.IsType<OkResult>(response);
            Assert.NotNull(result);
            this.locationServiceMock.Verify(a => a.RequestSynchronization(), Times.Once);
        }
    }
}