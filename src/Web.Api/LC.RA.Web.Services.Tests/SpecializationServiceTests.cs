using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Moq;

using ReviewApp.Web.Core.Domain;
using ReviewApp.Web.DataAccess.Contracts;
using ReviewApp.Web.Services.Contracts;

using Xunit;

namespace ReviewApp.Web.Services.Tests
{
    public class SpecializationServiceTests
    {
        private readonly ISpecializationService sut;

        private readonly Mock<ISpecializationRepository> specializationRepositoryMock;

        public SpecializationServiceTests()
        {
            this.specializationRepositoryMock = new Mock<ISpecializationRepository>();
            this.sut = new SpecializationService(this.specializationRepositoryMock.Object);
        }

        [Fact]
        public async void GetAll_WhenLocationsExist_ShouldReturnNotEmptyResult_Test()
        {
            // Arrange
            var specializations = new List<Specialization>
            {
                new Specialization()
            };
            this.specializationRepositoryMock
                .Setup(a => a.GetAllAsync())
                .Returns(Task.FromResult<IEnumerable<Specialization>>(specializations));

            // Act
            var result = await this.sut.GetAllAsync();

            // Assert
            Assert.True(result.Any());
            this.specializationRepositoryMock.Verify(a => a.GetAllAsync(), Times.Once);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public async void GetBySearchCriteriaAsync_WhenSearchCriteriaIsNullOrEmpty_ShouldThrowException_Test(string searchCriteria)
        {
            // Arrange
            // Act
            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => this.sut.GetBySearchCriteriaAsync(searchCriteria));
            this.specializationRepositoryMock.Verify(a => a.GetAllAsync(), Times.Never);
        }

        [Fact]
        public async void GetBySearchCriteriaAsync_WhenMatchedLocationsExist_ShouldReturnNotEmptyResult_Test()
        {
            // Arrange
            var specializations = new List<Specialization>
            {
                new Specialization()
            };
            this.specializationRepositoryMock
                .Setup(a => a.GetBySearchCriteriaAsync(It.IsAny<string>()))
                .Returns(Task.FromResult<IEnumerable<Specialization>>(specializations));

            // Act
            var result = await this.sut.GetBySearchCriteriaAsync("Oculist");

            // Assert
            Assert.True(result.Any());
            this.specializationRepositoryMock.Verify(a => a.GetBySearchCriteriaAsync(It.IsAny<string>()), Times.Once);
        }
    }
}