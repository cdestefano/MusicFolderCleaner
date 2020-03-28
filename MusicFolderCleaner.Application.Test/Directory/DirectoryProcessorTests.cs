using Moq;
using MusicFolderCleaner.Application.Directory;
using Xunit;

namespace MusicFolderCleaner.Application.Test.Directory
{
    public class DirectoryProcessorTests
    {
        public DirectoryProcessorTests()
        {
            var directories = new[]
            {
                Directory1,
                Directory2,
                Directory3,
                Directory4,
                Directory5
            };
            _directory.Setup(x => x.GetDirectories(It.IsAny<string>())).Returns(directories);

            _directoryProcessor = new DirectoryProcessor(_directory.Object, _directoryValidator.Object);
        }

        private readonly Mock<IDirectory> _directory = new Mock<IDirectory>();
        private readonly Mock<IDirectoryValidator> _directoryValidator = new Mock<IDirectoryValidator>();
        private const string FileExtensionToLookFor = ".flac";
        private const string FilePath = "C:\\test\\path";
        private const string DestinationPath = "C:\\testFileDirectoryToMoveTo\\path";
        private const string Directory1 = "directory1";
        private const string Directory2 = "directory2/subDirectory2";
        private const string Directory3 = "directory3/subDirectory3";
        private const string Directory4 = "directory4/subDirectory4/subSubDirectory4";
        private const string Directory5 = "directory5";

        private readonly IDirectoryProcessor _directoryProcessor;

        [Fact]
        public void WhenCallingProcess_WithAFileExtensionThatDoesNotExist_ThenItShouldNotDeleteAllDirectories()
        {
            // Arrange
            _directoryValidator.Setup(x => x.Validate(Directory1, FileExtensionToLookFor)).Returns(false);
            _directoryValidator.Setup(x => x.Validate(Directory2, FileExtensionToLookFor)).Returns(false);
            _directoryValidator.Setup(x => x.Validate(Directory3, FileExtensionToLookFor)).Returns(false);
            _directoryValidator.Setup(x => x.Validate(Directory4, FileExtensionToLookFor)).Returns(false);
            _directoryValidator.Setup(x => x.Validate(Directory5, FileExtensionToLookFor)).Returns(false);

            // Act
            _directoryProcessor.Process(FilePath, DestinationPath, FileExtensionToLookFor);

            // Assert
            _directory.Verify(x => x.Move(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(5));
        }

        [Fact]
        public void WhenCallingProcess_WithAFileExtensionThatExists_ThenItShouldNotDeleteThatDirectory()
        {
            // Arrange
            _directoryValidator.Setup(x => x.Validate(Directory1, FileExtensionToLookFor)).Returns(false);
            _directoryValidator.Setup(x => x.Validate(Directory2, FileExtensionToLookFor)).Returns(true);
            _directoryValidator.Setup(x => x.Validate(Directory3, FileExtensionToLookFor)).Returns(false);
            _directoryValidator.Setup(x => x.Validate(Directory4, FileExtensionToLookFor)).Returns(false);
            _directoryValidator.Setup(x => x.Validate(Directory5, FileExtensionToLookFor)).Returns(false);

            // Act
            _directoryProcessor.Process(FilePath, DestinationPath, FileExtensionToLookFor);

            // Assert
            _directory.Verify(x => x.Move(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(4));
        }

        [Fact]
        public void
            WhenCallingProcess_WithAFileExtensionThatExistsInAllDirectories_ThenItShouldNotDeleteAnyDirectories()
        {
            // Arrange
            _directoryValidator.Setup(x => x.Validate(Directory1, FileExtensionToLookFor)).Returns(true);
            _directoryValidator.Setup(x => x.Validate(Directory2, FileExtensionToLookFor)).Returns(true);
            _directoryValidator.Setup(x => x.Validate(Directory3, FileExtensionToLookFor)).Returns(true);
            _directoryValidator.Setup(x => x.Validate(Directory4, FileExtensionToLookFor)).Returns(true);
            _directoryValidator.Setup(x => x.Validate(Directory5, FileExtensionToLookFor)).Returns(true);

            // Act
            _directoryProcessor.Process(FilePath, DestinationPath, FileExtensionToLookFor);

            // Assert
            _directory.Verify(x => x.Move(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }
    }
}