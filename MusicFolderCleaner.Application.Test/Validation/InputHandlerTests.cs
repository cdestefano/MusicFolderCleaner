using System;
using Moq;
using MusicFolderCleaner.Application.Directory;
using MusicFolderCleaner.Application.Validation;
using Xunit;

namespace MusicFolderCleaner.Application.Test.Validation
{
    public class InputHandlerTests
    {
        private readonly Mock<IDirectory> _directory = new Mock<IDirectory>();
        private readonly Mock<IErrorLogger> _logger = new Mock<IErrorLogger>();

        private const string InvalidDirectory = "C:\\directory1\\input.txt";
        private const string ValidDirectory = "C:\\directory1\\directory2";
        private const string FileExtensionToLookFor = ".flac";

        private readonly IInputHandler _inputHandler;

        public InputHandlerTests()
        {
            SetupDirectoryValues();

            _inputHandler = new InputHandler(_logger.Object, _directory.Object);
        }

        private void SetupDirectoryValues()
        {
            _directory.Setup(x => x.Exists(ValidDirectory)).Returns(true);
            _directory.Setup(x => x.Exists(InvalidDirectory)).Returns(false);
        }

        [Fact]
        public void WhenCallingValidate_WithValidInput_WillReturnTrue()
        {
            // Arrange
            var inputArguments = new[]
            {
                ValidDirectory,
                ValidDirectory,
                FileExtensionToLookFor
            };

            // Act
            var result = _inputHandler.Validate(inputArguments);

            // Assert
            Assert.True(result);
            _logger.Verify(x => x.LogError(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void WhenCallingValidate_WithStringSeparators_WillReturnTrue()
        {
            // Arrange
            var inputArguments = new[]
            {
                $"\'{ValidDirectory}\'",
                $"\'{ValidDirectory}\'",
                $"\'{FileExtensionToLookFor}\'"
            };

            // Act
            var result = _inputHandler.Validate(inputArguments);

            // Assert
            Assert.True(result);
            _logger.Verify(x => x.LogError(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void WhenCallingValidate_WithTheHelpCommand_InTheSecondArgument()
        {
            // Arrange
            var inputArguments = new[]
            {
                ValidDirectory,
                "test -help"
            };

            // Act
            var result = _inputHandler.Validate(inputArguments);

            // Assert
            Assert.False(result);
            _logger.Verify(x => x.LogError(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void WhenCallingValidate_WithAQuestionMark_AsTheOnlyCommand()
        {
            // Arrange
            var inputArguments = new[]
            {
                "?"
            };

            // Act
            var result = _inputHandler.Validate(inputArguments);

            // Assert
            Assert.False(result);
            _logger.Verify(x => x.LogError(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void WhenCallingValidate_WithNoArguments_WillReturnFalse()
        {
            // Arrange
            var inputArguments = Array.Empty<string>();

            // Act
            var result = _inputHandler.Validate(inputArguments);

            // Assert
            Assert.False(result);
            _logger.Verify(x => x.LogError(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void WhenCallingValidate_WithFiveArguments_WillReturnFalse()
        {
            // Arrange
            var inputArguments = new[]
            {
                ValidDirectory,
                ValidDirectory,
                "input3",
                "input4",
                "input5"
            };

            // Act
            var result = _inputHandler.Validate(inputArguments);

            // Assert
            Assert.False(result);
            _logger.Verify(x => x.LogError(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void WhenCallingValidate_WithInvalidDirectory_WillReturnFalse()
        {
            // Arrange
            var inputArguments = new[]
            {
                InvalidDirectory,
                ValidDirectory,
                FileExtensionToLookFor
            };

            // Act
            var result = _inputHandler.Validate(inputArguments);

            // Assert
            Assert.False(result);
            _logger.Verify(x => x.LogError(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void WhenCallingValidate_WhenTheSecondParameterDoesNotExistButIsCreated_WillReturnTrue()
        {
            // Arrange
            var inputArguments = new[]
            {
                ValidDirectory,
                InvalidDirectory,
                ".flac"
            };

            _directory.Setup(x => x.CreateDirectory(InvalidDirectory)).Returns(true);

            // Act
            var result = _inputHandler.Validate(inputArguments);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void WhenCallingValidate_WithAFileThatDoesNotContainAnExtension_WillReturnFalse()
        {
            // Arrange
            var inputArguments = new[]
            {
                InvalidDirectory,
                InvalidDirectory,
                "flac"
            };

            // Act
            var result = _inputHandler.Validate(inputArguments);

            // Assert
            Assert.False(result);
            _logger.Verify(x => x.LogError(It.IsAny<string>()), Times.Once);
        }
    }
}