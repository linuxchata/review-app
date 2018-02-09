using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LC.RA.WebApi.Core.Domain;
using LC.RA.WebApi.DataAccess.Contracts;
using LC.RA.WebApi.Services.Contracts;
using Moq;
using Xunit;

namespace LC.RA.WebApi.Services.Tests
{
    public class SubjectServiceTests
    {
        private readonly ISubjectService sut;

        private readonly Mock<IDoctorRepository> doctorRepositoryMock;

        public SubjectServiceTests()
        {
            this.doctorRepositoryMock = new Mock<IDoctorRepository>();
            this.sut = new SubjectService(this.doctorRepositoryMock.Object);
        }

        [Fact]
        public async void GetAllAsync_WhenSubjectsExist_ShouldReturnNotEmptyResult_Test()
        {
            // Arrange
            var subjects = new List<Doctor>
            {
                new Doctor()
            };
            this.doctorRepositoryMock
                .Setup(a => a.GetAllAsync())
                .Returns(Task.FromResult<IEnumerable<Doctor>>(subjects));

            // Act
            var result = await this.sut.GetAllAsync();

            // Assert
            Assert.True(result.Any());
            this.doctorRepositoryMock.Verify(a => a.GetAllAsync(), Times.Once);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public async void GetByIdAsync_WhenIdIsNullOrEmpty_ShouldThrowException_Test(string id)
        {
            // Arrange
            // Act
            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => this.sut.GetByIdAsync(id));
            this.doctorRepositoryMock.Verify(a => a.GetByIdAsync(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async void GetByIdAsync_WhenSubjectExist_ShouldReturnNotEmptyResult_Test()
        {
            // Arrange
            var subject = new Doctor();
            this.doctorRepositoryMock
                .Setup(a => a.GetByIdAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(subject));

            // Act
            var result = await this.sut.GetByIdAsync("5a3c1c53cc849c169c9d6d81");

            // Assert
            Assert.NotNull(result);
            this.doctorRepositoryMock.Verify(a => a.GetByIdAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async void ExistsAsync_WhenSubjectIsNull_ShouldThrowException_Test()
        {
            // Arrange
            // Act
            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => this.sut.ExistsAsync(null));
            this.doctorRepositoryMock.Verify(a => a.GetByNamesAsync(It.IsAny<Doctor>()), Times.Never);
        }

        [Fact]
        public async void ExistsAsync_WhenSubjectExists_ShouldReturnFalse_Test()
        {
            // Arrange
            var subjects = new List<Doctor>();
            this.doctorRepositoryMock
                .Setup(a => a.GetByNamesAsync(It.IsAny<Doctor>()))
                .Returns(Task.FromResult(subjects.AsEnumerable()));

            // Act
            var result = await this.sut.ExistsAsync(new Doctor());

            // Assert
            Assert.False(result);
            this.doctorRepositoryMock.Verify(a => a.GetByNamesAsync(It.IsAny<Doctor>()), Times.Once);
        }

        [Fact]
        public async void ExistsAsync_WhenSubjectExists_ShouldReturnTrue_Test()
        {
            // Arrange
            var subjects = new List<Doctor>
            {
                new Doctor()
            };
            this.doctorRepositoryMock
                .Setup(a => a.GetByNamesAsync(It.IsAny<Doctor>()))
                .Returns(Task.FromResult(subjects.AsEnumerable()));

            // Act
            var result = await this.sut.ExistsAsync(new Doctor());

            // Assert
            Assert.True(result);
            this.doctorRepositoryMock.Verify(a => a.GetByNamesAsync(It.IsAny<Doctor>()), Times.Once);
        }

        [Fact]
        public async void CreateAsync_WhenSubjectIsNull_ShouldThrowException_Test()
        {
            // Arrange
            // Act
            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => this.sut.CreateAsync(null));
            this.doctorRepositoryMock.Verify(a => a.InsertAsync(It.IsAny<Doctor>(), string.Empty), Times.Never);
        }

        [Fact]
        public async void CreateAsync_WhenSubjectIsValid_ShouldNotThrowException_Test()
        {
            // Arrange
            // Act
            // Assert
            await this.sut.CreateAsync(new Doctor());
            this.doctorRepositoryMock.Verify(a => a.InsertAsync(It.IsAny<Doctor>(), string.Empty), Times.Once);
        }

        [Fact]
        public async void UpdateAsync_WhenSubjectIsNull_ShouldThrowException_Test()
        {
            // Arrange
            // Act
            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => this.sut.UpdateAsync(null));
            this.doctorRepositoryMock.Verify(a => a.UpdateAsync(It.IsAny<Doctor>(), string.Empty), Times.Never);
        }

        [Fact]
        public async void UpdateAsync_WhenSubjectIsValid_ShouldNotThrowException_Test()
        {
            // Arrange
            // Act
            // Assert
            await this.sut.UpdateAsync(new Doctor());
            this.doctorRepositoryMock.Verify(a => a.UpdateAsync(It.IsAny<Doctor>(), string.Empty), Times.Once);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public async void RemoveAsync_WhenSubjectIdIsNullOrEmpty_ShouldThrowException_Test(string subjectId)
        {
            // Arrange
            // Act
            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => this.sut.DeleteAsync(subjectId));
            this.doctorRepositoryMock.Verify(a => a.DeleteAsync(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async void RemoveAsync_WhenSubjectIsValid_ShouldNotThrowException_Test()
        {
            // Arrange
            // Act
            // Assert
            await this.sut.DeleteAsync("5a3c19155fbfbc1518f3759f");
            this.doctorRepositoryMock.Verify(a => a.DeleteAsync(It.IsAny<string>()), Times.Once);
        }
    }
}