using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using ReviewSystem.Core;
using ReviewSystem.DataAccess.Contracts;
using ReviewSystem.Services.Contracts;
using Xunit;

namespace ReviewSystem.Services.Tests
{
    public class SubjectServiceTests
    {
        private readonly ISubjectService sut;

        private readonly Mock<IModifyRepository<Doctor>> modifyRepositoryMock;

        public SubjectServiceTests()
        {
            this.modifyRepositoryMock = new Mock<IModifyRepository<Doctor>>();
            this.sut = new SubjectService(this.modifyRepositoryMock.Object);
        }

        [Fact]
        public async void GetAllAsync_WhenSubjectsExist_ShouldReturnNotEmptyResult_Test()
        {
            // Arrange
            var subjects = new List<Doctor>
            {
                new Doctor()
            };
            this.modifyRepositoryMock
                .Setup(a => a.GetAllAsync())
                .Returns(Task.FromResult<IEnumerable<Doctor>>(subjects));

            // Act
            var result = await this.sut.GetAllAsync();

            // Assert
            Assert.True(result.Any());
            this.modifyRepositoryMock.Verify(a => a.GetAllAsync(), Times.Once);
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
            this.modifyRepositoryMock.Verify(a => a.GetByIdAsync(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async void GetByIdAsync_WhenSubjectExist_ShouldReturnNotEmptyResult_Test()
        {
            // Arrange
            var subject = new Doctor();
            this.modifyRepositoryMock
                .Setup(a => a.GetByIdAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(subject));

            // Act
            var result = await this.sut.GetByIdAsync("5a3c1c53cc849c169c9d6d81");

            // Assert
            Assert.NotNull(result);
            this.modifyRepositoryMock.Verify(a => a.GetByIdAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async void CreateAsync_WhenSubjectIsNull_ShouldThrowException_Test()
        {
            // Arrange
            // Act
            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => this.sut.CreateAsync(null));
            this.modifyRepositoryMock.Verify(a => a.InsertAsync(It.IsAny<Doctor>(), string.Empty), Times.Never);
        }

        [Fact]
        public async void CreateAsync_WhenSubjectIsValid_ShouldNotThrowException_Test()
        {
            // Arrange
            // Act
            // Assert
            await this.sut.CreateAsync(new Doctor());
            this.modifyRepositoryMock.Verify(a => a.InsertAsync(It.IsAny<Doctor>(), string.Empty), Times.Once);
        }

        [Fact]
        public async void UpdateAsync_WhenSubjectIsNull_ShouldThrowException_Test()
        {
            // Arrange
            // Act
            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => this.sut.UpdateAsync(null));
            this.modifyRepositoryMock.Verify(a => a.UpdateAsync(It.IsAny<Doctor>(), string.Empty), Times.Never);
        }

        [Fact]
        public async void UpdateAsync_WhenSubjectIsValid_ShouldNotThrowException_Test()
        {
            // Arrange
            // Act
            // Assert
            await this.sut.UpdateAsync(new Doctor());
            this.modifyRepositoryMock.Verify(a => a.UpdateAsync(It.IsAny<Doctor>(), string.Empty), Times.Once);
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
            this.modifyRepositoryMock.Verify(a => a.DeleteAsync(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async void RemoveAsync_WhenSubjectIsValid_ShouldNotThrowException_Test()
        {
            // Arrange
            // Act
            // Assert
            await this.sut.DeleteAsync("5a3c19155fbfbc1518f3759f");
            this.modifyRepositoryMock.Verify(a => a.DeleteAsync(It.IsAny<string>()), Times.Once);
        }
    }
}