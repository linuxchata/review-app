using Microsoft.AspNetCore.Mvc;

using ReviewApp.Location.Api.Controllers;

using Xunit;

namespace ReviewApp.Location.Tests.Api.Controllers
{
    public class HomeControllerTests
    {
        private readonly HomeController sut;

        public HomeControllerTests()
        {
            this.sut = new HomeController();
        }

        [Fact]
        public void Index_ShouldReturnOkResult_Test()
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