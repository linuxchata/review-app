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

        private readonly Mock<ISubjectRepository> subjectRepositoryMock;

        public SubjectServiceTests()
        {
            this.subjectRepositoryMock = new Mock<ISubjectRepository>();
            this.sut = new SubjectService(this.subjectRepositoryMock.Object);
        }

        [Fact]
        public async void GetAllAsync_WhenSubjectsExist_ShouldReturnNotEmptyResult_Test()
        {
            // Arrange
            var subjects = new List<Subject>
            {
                new Subject()
            };
            this.subjectRepositoryMock
                .Setup(a => a.GetAllAsync())
                .Returns(Task.FromResult<IEnumerable<Subject>>(subjects));

            // Act
            var result = await this.sut.GetAllAsync();

            // Assert
            Assert.True(result.Any());
            this.subjectRepositoryMock.Verify(a => a.GetAllAsync(), Times.Once);
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
            this.subjectRepositoryMock.Verify(a => a.GetByIdAsync(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async void GetByIdAsync_WhenSubjectExist_ShouldReturnNotEmptyResult_Test()
        {
            // Arrange
            var subject = new Subject();
            this.subjectRepositoryMock
                .Setup(a => a.GetByIdAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(subject));

            // Act
            var result = await this.sut.GetByIdAsync("5a3c1c53cc849c169c9d6d81");

            // Assert
            Assert.NotNull(result);
            this.subjectRepositoryMock.Verify(a => a.GetByIdAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async void AddAsync_WhenSubjectIsNull_ShouldThrowException_Test()
        {
            // Arrange
            // Act
            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => this.sut.AddAsync(null));
            this.subjectRepositoryMock.Verify(a => a.InsertAsync(It.IsAny<Subject>()), Times.Never);
        }

        [Fact]
        public async void AddAsync_WhenSubjectIsValid_ShouldNotThrowException_Test()
        {
            // Arrange
            // Act
            // Assert
            await this.sut.AddAsync(new Subject());
            this.subjectRepositoryMock.Verify(a => a.InsertAsync(It.IsAny<Subject>()), Times.Once);
        }

        [Fact]
        public async void EditAsync_WhenSubjectIsNull_ShouldThrowException_Test()
        {
            // Arrange
            // Act
            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => this.sut.EditAsync(null));
            this.subjectRepositoryMock.Verify(a => a.UpdateAsync(It.IsAny<Subject>()), Times.Never);
        }

        [Fact]
        public async void EditAsync_WhenSubjectIsValid_ShouldNotThrowException_Test()
        {
            // Arrange
            // Act
            // Assert
            await this.sut.EditAsync(new Subject());
            this.subjectRepositoryMock.Verify(a => a.UpdateAsync(It.IsAny<Subject>()), Times.Once);
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
            this.subjectRepositoryMock.Verify(a => a.DeleteAsync(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async void RemoveAsync_WhenSubjectIsValid_ShouldNotThrowException_Test()
        {
            // Arrange
            // Act
            // Assert
            await this.sut.DeleteAsync("5a3c19155fbfbc1518f3759f");
            this.subjectRepositoryMock.Verify(a => a.DeleteAsync(It.IsAny<string>()), Times.Once);
        }
    }
}