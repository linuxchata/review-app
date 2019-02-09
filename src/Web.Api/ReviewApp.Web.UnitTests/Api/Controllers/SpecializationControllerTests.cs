using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Moq;

using ReviewApp.Web.Api.Controllers;
using ReviewApp.Web.Core.Domain;
using ReviewApp.Web.Services.Contracts;

using Xunit;

namespace ReviewApp.Web.UnitTests.Api.Controllers
{
    public class SpecializationControllerTests
    {
        private readonly SpecializationController sut;

        private readonly Mock<ISpecializationService> specializationServiceMock;

        public SpecializationControllerTests()
        {
            this.specializationServiceMock = new Mock<ISpecializationService>();
            this.sut = new SpecializationController(this.specializationServiceMock.Object);
        }

        [Fact]
        public async void GetAll_WhenResultIsNotAvailable_ShouldReturnNoContent_Test()
        {
            // Arrange
            // Act
            var response = await this.sut.GetAll();

            // Assert
            Assert.IsType<NoContentResult>(response);
        }

        [Fact]
        public async void GetAll_WhenResultIsAvailable_ShouldReturnOk_Test()
        {
            // Arrange
            var specializations = new List<Specialization>
            {
                new Specialization
                {
                    Id = "5a3eddd75ac5641b4ca8e652",
                    Name = "Oculist"
                }
            };
            this.specializationServiceMock
                .Setup(a => a.GetAllAsync())
                .Returns(Task.FromResult<IEnumerable<Specialization>>(specializations))
                .Verifiable();

            // Act
            var response = await this.sut.GetAll();

            // Assert
            var result = Assert.IsType<OkObjectResult>(response);
            this.specializationServiceMock.Verify();
            Assert.Equal(specializations, result.Value);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public async void GetBySearchCriteria_WhenSearchCriteriaIsNullOrEmpty_ShouldReturnBadRequest_Test(string searchCriteria)
        {
            // Arrange
            // Act
            var response = await this.sut.GetBySearchCriteria(searchCriteria);

            // Assert
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public async void GetBySearchCriteria_WhenResultIsNotAvailable_ShouldReturnNotFound_Test()
        {
            // Arrange
            // Act
            var response = await this.sut.GetBySearchCriteria("Oculist");

            // Assert
            Assert.IsType<NotFoundResult>(response);
        }

        [Fact]
        public async void GetBySearchCriteria_WhenResultIsAvailable_ShouldReturnOk_Test()
        {
            // Arrange
            var specializations = new List<Specialization>
            {
                new Specialization
                {
                    Id = "5a3eddd75ac5641b4ca8e652",
                    Name = "Oculist"
                }
            };
            this.specializationServiceMock
                .Setup(a => a.GetBySearchCriteriaAsync(It.IsAny<string>()))
                .Returns(Task.FromResult<IEnumerable<Specialization>>(specializations))
                .Verifiable();

            // Act
            var response = await this.sut.GetBySearchCriteria("Kiev");

            // Assert
            var result = Assert.IsType<OkObjectResult>(response);
            this.specializationServiceMock.Verify();
            Assert.Equal(specializations, result.Value);
        }
    }
}