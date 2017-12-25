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
    public class DoctorControllerTests
    {
        private readonly DoctorController sut;

        private readonly Mock<IDoctorService> doctorServiceMock;

        public DoctorControllerTests()
        {
            this.doctorServiceMock = new Mock<IDoctorService>();
            this.sut = new DoctorController(this.doctorServiceMock.Object);
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
            var doctors = new List<Doctor>
            {
                new Doctor()
            };
            this.doctorServiceMock
                .Setup(a => a.GetAllAsync())
                .Returns(Task.FromResult<IEnumerable<Doctor>>(doctors))
                .Verifiable();

            // Act
            var response = await this.sut.Get();

            // Assert
            var result = Assert.IsType<OkObjectResult>(response);
            Assert.Equal(doctors, result.Value);
            this.doctorServiceMock.Verify();
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
            this.doctorServiceMock.Verify();
        }

        [Fact]
        public async void Get_WhenIdIsValidAndResultIsAvailable_ShouldReturnOk_Test()
        {
            // Arrange
            var doctor = new Doctor();
            this.doctorServiceMock
                .Setup(a => a.GetByIdAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(doctor))
                .Verifiable();

            // Act
            var response = await this.sut.Get("5a3c1c53cc849c169c9d6d81");

            // Assert
            var result = Assert.IsType<OkObjectResult>(response);
            Assert.Equal(doctor, result.Value);
            this.doctorServiceMock.Verify();
        }

        [Fact]
        public async void Add_WhenDoctorIsNull_ShouldReturnBadRequest_Test()
        {
            // Arrange
            // Act
            var response = await this.sut.Add(null);

            // Assert
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public async void Add_WhenDoctorIsValid_ShouldReturnCreated_Test()
        {
            // Arrange
            var doctor = new Doctor();
            this.doctorServiceMock
                .Setup(a => a.AddAsync(It.IsAny<Doctor>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            // Act
            var response = await this.sut.Add(doctor);

            // Assert
            Assert.IsType<CreatedResult>(response);
            Assert.Equal("GetDoctor", ((CreatedResult)response).Location);
            this.doctorServiceMock.Verify();
        }

        [Fact]
        public async void Edit_WhenDoctorIsNull_ShouldReturnBadRequest_Test()
        {
            // Arrange
            // Act
            var response = await this.sut.Edit(string.Empty, null);

            // Assert
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public async void Edit_WhenIdAndDoctorIdAreNotMatch_ShouldReturnBadRequest_Test()
        {
            // Arrange
            var doctor = new Doctor
            {
                Id = "5a3c19155fbfbc1518f3759f"
            };

            // Act
            var response = await this.sut.Edit("some_other_id", doctor);

            // Assert
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public async void Delete_WhenParametersAreValid_ShouldReturnNoContent_Test()
        {
            // Arrange
            var doctor = new Doctor
            {
                Id = "5a3c19155fbfbc1518f3759f"
            };

            this.doctorServiceMock
                .Setup(a => a.EditAsync(It.IsAny<Doctor>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            // Act
            var response = await this.sut.Edit("5a3c19155fbfbc1518f3759f", doctor);

            // Assert
            Assert.IsType<NoContentResult>(response);
            this.doctorServiceMock.Verify();
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
            this.doctorServiceMock
                .Setup(a => a.DeleteAsync(It.IsAny<string>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            // Act
            var response = await this.sut.Delete("5a3c19155fbfbc1518f3759f");

            // Assert
            Assert.IsType<NoContentResult>(response);
            this.doctorServiceMock.Verify();
        }
    }
}
