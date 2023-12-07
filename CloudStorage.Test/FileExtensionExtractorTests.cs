using CloudStorage.Core.Utils;
using NUnit.Framework;

namespace CloudStorage.Test
{
    public class FileExtensionExtractorTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase("file.jpg", ".jpg")]
        [TestCase("another.file.png", ".png")]
        [TestCase("another", "")]
        [TestCase("another.file", ".file")]
        public void Extract_When_Called_Return_Correct_Value(string input, string expected)
        {
            // Act
            var actual = FileNameHelper.ExtractExtension(input);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}