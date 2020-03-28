using System.IO;
using Moq;
using MusicFolderCleaner.Application.Directory;
using Xunit;

namespace MusicFolderCleaner.Application.Test.Directory
{
    public class DirectoryValidatorTests
    {
        private readonly Mock<IDirectory> _directory = new Mock<IDirectory>();

        private const string FileExtensionToLookFor = ".flac";
        private const string FilePath = "C:\\test\\path";

        [Fact]
        public void WhenCallingValidate_WithAMatchingExtension_WillReturnTrue()
        {
            // Arrange
            var files = new[]
            {
                "dir1/testFile1.mp3",
                "dir2/testFile2.flac",
                "testFile3.mp3"
            };

            _directory.Setup(x => x.GetFiles(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<SearchOption>()))
                .Returns(files);

            var directoryValidator = new DirectoryValidator(_directory.Object);

            // Act
            var result = directoryValidator.Validate(FilePath, FileExtensionToLookFor);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void WhenCallingValidate_WithOutAMatchingExtension_WillReturnFalse()
        {
            // Arrange
            var files = new[]
            {
                "dir1/testFile1.mp3",
                "dir2/testFile2.mp3",
                "testFile3.mp3"
            };

            _directory.Setup(x => x.GetFiles(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<SearchOption>()))
                .Returns(files);

            var directoryValidator = new DirectoryValidator(_directory.Object);

            // Act
            var result = directoryValidator.Validate(FilePath, FileExtensionToLookFor);

            // Assert
            Assert.False(result);
        }
    }
}