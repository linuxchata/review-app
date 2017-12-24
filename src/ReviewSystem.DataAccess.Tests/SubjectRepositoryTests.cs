using System.Linq;
using MongoDB.Bson;
using ReviewSystem.Core;
using ReviewSystem.DataAccess.Contracts;
using Xunit;

namespace ReviewSystem.DataAccess.Tests
{
    [Trait("Category", "IntegrationTests")]
    public class SubjectRepositoryTests
    {
        private readonly Subject testSubject;

        private IDatabaseConnection databaseConnection;

        public SubjectRepositoryTests()
        {
            this.testSubject = new Subject
            {
                FirstName = "John",
                LastName = "Oliver",
                Address = "New York",
                Degree = "Master",
                Specialization = "Surgeon"
            };
        }

        [Fact]
        public void GetAllAsync_ShouldReturnNotNullResult_Test()
        {
            // Arrange
            var sut = new SubjectRepository(this.GetDatabaseConnection());

            // Act
            var result = sut.GetAllAsync().Result;

            // Assert
            var subjects = result.ToList();
            Assert.NotNull(subjects);
        }

        [Fact]
        public void GetByIdAsync_WhenIdIsValid_ShouldReturnNotNullResult_Test()
        {
            // Arrange
            var sut = new SubjectRepository(this.GetDatabaseConnection());

            // Act
            var result = sut.GetByIdAsync("5a3c1c53cc849c169c9d6d81").Result;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async void InsertAsync_WhenSubjectIsValid_ShouldInsertSubject_Test()
        {
            // Arrange
            var sut = new SubjectRepository(this.GetDatabaseConnection());

            // Act
            await sut.InsertAsync(this.testSubject);

            // Assert
            Assert.NotNull(this.testSubject.Id);
        }

        [Fact]
        public async void UpdateAsync_WhenSubjectExists_ShouldUpdateSubject_Test()
        {
            // Arrange
            var sut = new SubjectRepository(this.GetDatabaseConnection());
            await sut.InsertAsync(this.testSubject);
            this.testSubject.FirstName = "Valentino";
            this.testSubject.LastName = "Rossi";

            // Act
            await sut.UpdateAsync(this.testSubject);

            // Assert
            var subjects = sut.GetAllAsync().Result.ToList();
            var updatedResult = subjects.First(a => a.Id == this.testSubject.Id);
            Assert.Equal("Valentino", updatedResult.FirstName);
            Assert.Equal("Rossi", updatedResult.LastName);
        }

        [Fact]
        public async void UpdateAsync_WhenSubjectDoesNotExist_ShouldNotUpdateSubject_Test()
        {
            // Arrange
            var sut = new SubjectRepository(this.GetDatabaseConnection());

            // Act
            await sut.UpdateAsync(this.testSubject);

            // Assert
            var subjects = sut.GetAllAsync().Result.ToList();
            var updatedResult = subjects.FirstOrDefault(a => a.Id == this.testSubject.Id);
            Assert.Null(updatedResult);
        }

        [Fact]
        public async void DeleteAsync_WhenSubjectExists_ShouldDeleteSubject_Test()
        {
            // Arrange
            var sut = new SubjectRepository(this.GetDatabaseConnection());
            await sut.InsertAsync(this.testSubject);

            // Act
            await sut.DeleteAsync(this.testSubject.Id);

            // Assert
            var subjects = sut.GetAllAsync().Result.ToList();
            Assert.False(subjects.Any(a => string.Equals(a.Id, this.testSubject.Id)));
        }

        [Fact]
        public async void DeleteAsync_WhenSubjectDoesNotExist_ShouldNotDeleteSubject_Test()
        {
            // Arrange
            var sut = new SubjectRepository(this.GetDatabaseConnection());
            var id = ObjectId.GenerateNewId().ToString();
            var beforeCount = sut.GetAllAsync().Result.Count();

            // Act
            await sut.DeleteAsync(id);

            // Assert
            var afterCount = sut.GetAllAsync().Result.Count();
            Assert.Equal(beforeCount, afterCount);
        }

        private IDatabaseConnection GetDatabaseConnection() =>
            this.databaseConnection ?? (this.databaseConnection = new TestDatabaseConnection());
    }
}
