using Microsoft.AspNetCore.Mvc;

using ReviewApp.Web.Api.Controllers;

using Xunit;

namespace ReviewApp.Web.Api.Tests.Controllers
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