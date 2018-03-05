using LC.RA.SynchronizationService.Api.Models.Application.Wikipedia;
using Xunit;

namespace LC.RA.SynchronizationService.Api.Tests
{
    public class ComparerByStartIndexTests
    {
        private readonly ComparerByStartIndex sut;

        public ComparerByStartIndexTests()
        {
            this.sut = new ComparerByStartIndex();
        }

        [Fact]
        public void Compare_WhenElementAreEqual_ShouldReturnZero_Test()
        {
            // Arrange
            var x = new WikiPageElement
            {
                StartIndex = 0
            };
            var y = new WikiPageElement
            {
                StartIndex = 0
            };

            // Act
            var result = this.sut.Compare(x, y);

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void Compare_WhenLeftElementHasBiggerStartIndex_ShouldReturnOne_Test()
        {
            // Arrange
            var x = new WikiPageElement
            {
                StartIndex = 100
            };
            var y = new WikiPageElement
            {
                StartIndex = 0
            };

            // Act
            var result = this.sut.Compare(x, y);

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public void Compare_WhenLeftElementHasSmallerStartIndex_ShouldReturnMinusOne_Test()
        {
            // Arrange
            var x = new WikiPageElement
            {
                StartIndex = 100
            };
            var y = new WikiPageElement
            {
                StartIndex = 500
            };

            // Act
            var result = this.sut.Compare(x, y);

            // Assert
            Assert.Equal(-1, result);
        }
    }
}
