using System.IO;
using System.Linq;

namespace MusicFolderCleaner.Application.Directory
{
    /// <inheritdoc cref="IDirectoryValidator" />
    public class DirectoryValidator : IDirectoryValidator
    {
        private readonly IDirectory _directory;

        /// <summary>
        /// Creates a new <see cref="IDirectoryValidator" />
        /// </summary>
        /// <param name="directory">The directory validator.</param>
        public DirectoryValidator(IDirectory directory)
        {
            _directory = directory;
        }

        /// <inheritdoc cref="IDirectoryValidator.Validate" />
        public bool Validate(string filePath, string fileExtension)
        {
            var files = _directory.GetFiles(filePath, "*.*", SearchOption.AllDirectories);

            return files.Any(file => file.EndsWith(fileExtension));
        }
    }
}