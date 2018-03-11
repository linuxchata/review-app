using LC.RA.Location.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace LC.RA.Location.Api.Tests.Controllers
{
    public class HomeControllerTests
    {
        private readonly HomeController sut;

        public HomeControllerTests()
        {
            this.sut = new HomeController();
        }

        [Fact]
        public void Synchronize_ShouldReturnOkResult_Test()
        {
            // Arrange
            // Act
            var response = this.sut.Index();

            // Assert
            var result = Assert.IsType<OkObjectResult>(response);
            Assert.NotNull(result);
        }
    }
}