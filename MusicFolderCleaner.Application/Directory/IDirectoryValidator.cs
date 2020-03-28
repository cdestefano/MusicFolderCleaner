namespace MusicFolderCleaner.Application.Directory
{
    /// <summary>
    /// Validates is the directory contains a certain file extension.
    /// </summary>
    public interface IDirectoryValidator
    {
        /// <summary>
        /// Determines if the given directory contains at least one of the given file extensions.
        /// </summary>
        /// <param name="filePath">The directory that is being checked.</param>
        /// <param name="fileExtension">The file extension that is being checked.</param>
        bool Validate(string filePath, string fileExtension);
    }
}