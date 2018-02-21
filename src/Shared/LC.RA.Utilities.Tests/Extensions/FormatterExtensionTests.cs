using System.Collections.Generic;
using LC.RA.Utilities.Extensions;
using Xunit;

namespace LC.RA.Utilities.Tests.Extensions
{
    public class FormatterExtensionTests
    {
        [Fact]
        public void Serialize_Deserialize_ShouldReturnInitialObjectGraph_Test()
        {
            // Arrange
            var objectGraph = new List<string> { "Test" };

            // Act
            var serialized = FormatterExtension.Serialize(objectGraph);
            var deserialized = FormatterExtension.Deserialize<List<string>>(serialized);

            // Assert
            Assert.Equal(objectGraph, deserialized);
        }
    }
}
