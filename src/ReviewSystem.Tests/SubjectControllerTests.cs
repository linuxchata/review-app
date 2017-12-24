using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ReviewSystem.Controllers;
using ReviewSystem.Core;
using ReviewSystem.Services.Contracts;
using Xunit;

namespace ReviewSystem.Tests
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
            var response = await this.sut.Get();

            // Assert
            Assert.IsType<NoContentResult>(response);
        }

        [Fact]
        public async void GetAll_WhenResultIsAvailable_ShouldReturnOk_Test()
        {
            // Arrange
            var subjects = new List<Subject>
            {
                new Subject()
            };
            this.subjectServiceMock
                .Setup(a => a.GetAllAsync())
                .Returns(Task.FromResult<IEnumerable<Subject>>(subjects))
                .Verifiable();

            // Act
            var response = await this.sut.Get();

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
            var subject = new Subject();
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
        public async void Add_WhenSubjectIsNull_ShouldReturnBadRequest_Test()
        {
            // Arrange
            // Act
            var response = await this.sut.Add(null);

            // Assert
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public async void Add_WhenSubjectIsValid_ShouldReturnCreated_Test()
        {
            // Arrange
            var subject = new Subject();
            this.subjectServiceMock
                .Setup(a => a.AddAsync(It.IsAny<Subject>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            // Act
            var response = await this.sut.Add(subject);

            // Assert
            Assert.IsType<CreatedResult>(response);
            Assert.Equal("GetSubject", ((CreatedResult)response).Location);
            this.subjectServiceMock.Verify();
        }

        [Fact]
        public async void Edit_WhenSubjectIsNull_ShouldReturnBadRequest_Test()
        {
            // Arrange
            // Act
            var response = await this.sut.Edit(string.Empty, null);

            // Assert
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public async void Edit_WhenIdAndSubjectIdAreNotMatch_ShouldReturnBadRequest_Test()
        {
            // Arrange
            var subject = new Subject
            {
                Id = "5a3c19155fbfbc1518f3759f"
            };

            // Act
            var response = await this.sut.Edit("some_other_id", subject);

            // Assert
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public async void Delete_WhenParametersAreValid_ShouldReturnNoContent_Test()
        {
            // Arrange
            var subject = new Subject
            {
                Id = "5a3c19155fbfbc1518f3759f"
            };

            this.subjectServiceMock
                .Setup(a => a.EditAsync(It.IsAny<Subject>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            // Act
            var response = await this.sut.Edit("5a3c19155fbfbc1518f3759f", subject);

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
        public async void Delete_WhenIdIsNotEmpty_ShouldReturnNoContent_Test()
        {
            // Arrange
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
