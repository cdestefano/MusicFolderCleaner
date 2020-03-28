namespace MusicFolderCleaner.Application.Directory
{
    /// <inheritdoc cref="IDirectoryProcessor" />
    public class DirectoryProcessor : IDirectoryProcessor
    {
        private readonly IDirectory _directory;
        private readonly IDirectoryValidator _directoryValidator;

        private string _fullSourcePath;
        private string _fullDestinationPath;
        private string _fileExtension;

        /// <summary>
        /// Creates a new <see cref="DirectoryProcessor" />
        /// </summary>
        /// <param name="directory">The directory wrapper for file modification.</param>
        /// <param name="directoryValidator">The directory validator to ensure the files are the correct format.</param>
        public DirectoryProcessor(IDirectory directory, IDirectoryValidator directoryValidator)
        {
            _directory = directory;
            _directoryValidator = directoryValidator;
        }

        /// <inheritdoc cref="IDirectoryProcessor.Process" />
        public void Process(string fullFilePath, string fullDestinationPath, string fileExtension)
        {
            _fullSourcePath = fullFilePath;
            _fullDestinationPath = fullDestinationPath;
            _fileExtension = fileExtension;

            var directories = _directory.GetDirectories(fullFilePath);
            foreach (var directory in directories)
            {
                ProcessDirectory(directory);
            }
        }

        private void ProcessDirectory(string directory)
        {
            if (!_directoryValidator.Validate(directory, _fileExtension))
            {
                var destinationToMoveTo = DestinationDirectory(directory);
                _directory.Move(directory, destinationToMoveTo);
            }
        }

        private string DestinationDirectory(string currentDirectory)
        {
            return _fullDestinationPath + currentDirectory.Replace(_fullSourcePath, string.Empty);
        }
    }
}