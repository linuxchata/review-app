﻿using System.Collections.Generic;
using System.Linq;
using ReviewSystem.Core.Application.Wikipedia;
using ReviewSystem.Services.Synchronization;
using Xunit;

namespace ReviewSystem.Services.Tests
{
    public class WikipediaParsingServiceTests
    {
        private readonly WikipediaParsingService sut;

        public WikipediaParsingServiceTests()
        {
            this.sut = new WikipediaParsingService();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ParsePage_WhenContentIsNullOrEmpty_ShouldReturnEmptyResult_Test(string content)
        {
            // Arrange
            // Act
            var result = this.sut.ParsePage(content);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void ParsePage_WhenContentIsValid_ShouldReturnResult_Test()
        {
            // Arrange
            // Act
            var result = this.sut.ParsePage(Properties.Resources.PageContent);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(5, result.Count);
        }

        [Fact]
        public void ParseTable_WhenPageContentIsNull_ShouldReturnEmptyResult_Test()
        {
            // Arrange
            // Act
            var result = this.sut.ParseTable(null);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void ParseTable_WhenPageContentIsWithoutTables_ShouldReturnEmptyResult_Test()
        {
            // Arrange
            var pageContent = this.sut.ParsePage(Properties.Resources.PageContent);
            var pageContentWithoutTables = pageContent.Where(a => a.ContentType != WikiPageContentType.Table);
            var page = new SortedSet<WikiPageElement>(pageContentWithoutTables, new ComparerByStartIndex());

            // Act
            var result = this.sut.ParseTable(page);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void ParseTable_WhenPageContentIsWithTable_ShouldReturnResult_Test()
        {
            // Arrange
            var pageContent = this.sut.ParsePage(Properties.Resources.PageContent);

            // Act
            var result = this.sut.ParseTable(pageContent);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(462, result.Count);
        }
    }
}