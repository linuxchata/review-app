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
            var response = await this.sut.GetAll();

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
            var response = await this.sut.GetAll();

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
        public async void Create_WhenDoctorIsNull_ShouldReturnBadRequest_Test()
        {
            // Arrange
            // Act
            var response = await this.sut.Create(null);

            // Assert
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public async void Create_WhenDoctorIsValid_ShouldReturnCreated_Test()
        {
            // Arrange
            var doctor = new Doctor
            {
                Id = "5a3c19155fbfbc1518f3759f"
            };

            this.doctorServiceMock
                .Setup(a => a.CreateAsync(It.IsAny<Doctor>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            // Act
            var response = await this.sut.Create(doctor);

            // Assert
            Assert.IsType<CreatedAtRouteResult>(response);
            Assert.Equal("GetDoctor", ((CreatedAtRouteResult)response).RouteName);
            Assert.Equal("5a3c19155fbfbc1518f3759f", ((CreatedAtRouteResult)response).RouteValues["id"]);
            this.doctorServiceMock.Verify();
        }

        [Fact]
        public async void Update_WhenDoctorIsNull_ShouldReturnBadRequest_Test()
        {
            // Arrange
            // Act
            var response = await this.sut.Update(string.Empty, null);

            // Assert
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public async void Update_WhenIdAndDoctorIdAreNotMatch_ShouldReturnBadRequest_Test()
        {
            // Arrange
            var doctor = new Doctor
            {
                Id = "5a3c19155fbfbc1518f3759f"
            };

            // Act
            var response = await this.sut.Update("some_other_id", doctor);

            // Assert
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public async void Update_WhenParametersAreValidAndDoctorDoesNotExist_ShouldReturnNotFound_Test()
        {
            // Arrange
            var doctor = new Doctor
            {
                Id = "5a3c19155fbfbc1518f3759f"
            };

            // Act
            var response = await this.sut.Update(doctor.Id, doctor);

            // Assert
            Assert.IsType<NotFoundResult>(response);
        }

        [Fact]
        public async void Update_WhenParametersAreValidAndDoctorExists_ShouldReturnCreated_Test()
        {
            // Arrange
            var doctor = new Doctor
            {
                Id = "5a3c19155fbfbc1518f3759f"
            };

            this.doctorServiceMock
                .Setup(a => a.GetByIdAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(doctor))
                .Verifiable();

            this.doctorServiceMock
                .Setup(a => a.UpdateAsync(It.IsAny<Doctor>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            // Act
            var response = await this.sut.Update(doctor.Id, doctor);

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
        public async void Delete_WhenIdIsNotEmptyAndDoctorDoesNotExist_ShouldReturnNotFound_Test()
        {
            // Arrange
            // Act
            var response = await this.sut.Delete("5a3c19155fbfbc1518f3759f");

            // Assert
            Assert.IsType<NotFoundResult>(response);
        }

        [Fact]
        public async void Delete_WhenIdIsNotEmptyAndDoctorExists_ShouldReturnNoContent_Test()
        {
            // Arrange
            var doctor = new Doctor
            {
                Id = "5a3c19155fbfbc1518f3759f"
            };

            this.doctorServiceMock
                .Setup(a => a.GetByIdAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(doctor))
                .Verifiable();

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
