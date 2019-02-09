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
    public class SubjectControllerTests
    {
        private readonly SubjectController sut;

        private readonly Mock<ISubjectService> subjectServiceMock;

        public SubjectControllerTests()
        {
            this.subjectServiceMock = new Mock<ISubjectService>();
            this.sut = new SubjectController(this.subjectServiceMock.Object);
        }

        [Fact]
        public async void GetAll_WhenResultIsNotAvailble_ShouldReturnNoContent_Test()
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
            var subjects = new List<Doctor>
            {
                new Doctor()
            };

            this.subjectServiceMock
                .Setup(a => a.GetAllAsync())
                .Returns(Task.FromResult<IEnumerable<Doctor>>(subjects))
                .Verifiable();

            // Act
            var response = await this.sut.GetAll();

            // Assert
            var result = Assert.IsType<OkObjectResult>(response);
            Assert.Equal(subjects, result.Value);
            this.subjectServiceMock.Verify();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public async void Get_WhenIdIsNullOrEmpty_ShouldReturnBadRequest_Test(string id)
        {
            // Arrange
            // Act
            var response = await this.sut.Get(id);

            // Assert
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public async void Get_WhenIdIsValidAndResultIsNotAvailable_ShouldReturnNotFound_Test()
        {
            // Arrange
            // Act
            var response = await this.sut.Get("5a3c1c53cc849c169c9d6d81");

            // Assert
            Assert.IsType<NotFoundResult>(response);
            this.subjectServiceMock.Verify();
        }

        [Fact]
        public async void Get_WhenIdIsValidAndResultIsAvailable_ShouldReturnOk_Test()
        {
            // Arrange
            var subject = new Doctor();
            this.subjectServiceMock
                .Setup(a => a.GetByIdAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(subject))
                .Verifiable();

            // Act
            var response = await this.sut.Get("5a3c1c53cc849c169c9d6d81");

            // Assert
            var result = Assert.IsType<OkObjectResult>(response);
            Assert.Equal(subject, result.Value);
            this.subjectServiceMock.Verify();
        }

        [Fact]
        public async void Create_WhenSubjectIsNull_ShouldReturnBadRequest_Test()
        {
            // Arrange
            // Act
            var response = await this.sut.Create(null);

            // Assert
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public async void Create_WhenSubjectAlreadtExists_ShouldReturnConflict_Test()
        {
            // Arrange
            var subject = new Doctor
            {
                Id = "5a3c19155fbfbc1518f3759f"
            };

            this.subjectServiceMock
                .Setup(a => a.ExistsAsync(It.IsAny<Doctor>()))
                .Returns(Task.FromResult(true))
                .Verifiable();

            // Act
            var response = await this.sut.Create(subject);

            // Assert
            Assert.IsType<ObjectResult>(response);
            Assert.Equal("409", ((ObjectResult)response).StatusCode.ToString());
            Assert.Equal("Subject with the same name has already been created", ((ObjectResult)response).Value);
            this.subjectServiceMock.Verify();
        }

        [Fact]
        public async void Create_WhenSubjectIsValid_ShouldReturnCreated_Test()
        {
            // Arrange
            var subject = new Doctor
            {
                Id = "5a3c19155fbfbc1518f3759f"
            };

            this.subjectServiceMock
                .Setup(a => a.CreateAsync(It.IsAny<Doctor>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            // Act
            var response = await this.sut.Create(subject);

            // Assert
            Assert.IsType<CreatedAtRouteResult>(response);
            Assert.Equal("GetSubject", ((CreatedAtRouteResult)response).RouteName);
            Assert.Equal("5a3c19155fbfbc1518f3759f", ((CreatedAtRouteResult)response).RouteValues["id"]);
            this.subjectServiceMock.Verify();
        }

        [Fact]
        public async void Update_WhenSubjectIsNull_ShouldReturnBadRequest_Test()
        {
            // Arrange
            // Act
            var response = await this.sut.Update(string.Empty, null);

            // Assert
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public async void Update_WhenIdAndSubjectIdAreNotMatch_ShouldReturnBadRequest_Test()
        {
            // Arrange
            var subject = new Doctor
            {
                Id = "5a3c19155fbfbc1518f3759f"
            };

            // Act
            var response = await this.sut.Update("some_other_id", subject);

            // Assert
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public async void Update_WhenParametersAreValidAndSubjectDoesNotExist_ShouldReturnNotFound_Test()
        {
            // Arrange
            var subject = new Doctor
            {
                Id = "5a3c19155fbfbc1518f3759f"
            };

            // Act
            var response = await this.sut.Update(subject.Id, subject);

            // Assert
            Assert.IsType<NotFoundResult>(response);
        }

        [Fact]
        public async void Update_WhenParametersAreValidAndSubjectExists_ShouldReturnCreated_Test()
        {
            // Arrange
            var subject = new Doctor
            {
                Id = "5a3c19155fbfbc1518f3759f"
            };

            this.subjectServiceMock
                .Setup(a => a.GetByIdAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(subject))
                .Verifiable();

            this.subjectServiceMock
                .Setup(a => a.UpdateAsync(It.IsAny<Doctor>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            // Act
            var response = await this.sut.Update(subject.Id, subject);

            // Assert
            Assert.IsType<NoContentResult>(response);
            this.subjectServiceMock.Verify();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public async void Delete_WhenIdIsNullOrEmpty_ShouldReturnBadRequest_Test(string id)
        {
            // Arrange
            // Act
            var response = await this.sut.Delete(id);

            // Assert
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public async void Delete_WhenIdIsNotEmptyAndSubjectDoesNotExist_ShouldReturnNotFound_Test()
        {
            // Arrange
            // Act
            var response = await this.sut.Delete("5a3c19155fbfbc1518f3759f");

            // Assert
            Assert.IsType<NotFoundResult>(response);
        }

        [Fact]
        public async void Delete_WhenIdIsNotEmptyAndSubjectExists_ShouldReturnNoContent_Test()
        {
            // Arrange
            var subject = new Doctor
            {
                Id = "5a3c19155fbfbc1518f3759f"
            };

            this.subjectServiceMock
                .Setup(a => a.GetByIdAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(subject))
                .Verifiable();

            this.subjectServiceMock
                .Setup(a => a.DeleteAsync(It.IsAny<string>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            // Act
            var response = await this.sut.Delete("5a3c19155fbfbc1518f3759f");

            // Assert
            Assert.IsType<NoContentResult>(response);
            this.subjectServiceMock.Verify();
        }
    }
}
