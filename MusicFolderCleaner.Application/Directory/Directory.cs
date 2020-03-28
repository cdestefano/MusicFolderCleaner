using System;
using MusicFolderCleaner.Application.Validation;

namespace MusicFolderCleaner.Application.Directory
{
    /// <inheritdoc cref="IDirectory"/>
    public class Directory : IDirectory
    {
        private readonly IErrorLogger _logger;

        /// <summary>
        /// Creates a new instance of the <see cref="Directory"/> class.
        /// </summary>
        /// <param name="logger">The logger for potential issues.</param>
        public Directory(IErrorLogger logger)
        {
            _logger = logger;
        }

        /// <inheritdoc cref="IDirectory.GetFiles"/>
        public string[] GetFiles(string path, string searchPattern, System.IO.SearchOption searchOption) => System.IO.Directory.GetFiles(path, searchPattern, searchOption);
        /// <inheritdoc cref="IDirectory.GetDirectories"/>
        public string[] GetDirectories(string path) => System.IO.Directory.GetDirectories(path);
        /// <inheritdoc cref="IDirectory.Exists"/>
        public bool Exists(string path) => System.IO.Directory.Exists(path);
        /// <inheritdoc cref="IDirectory.Move"/>
        public void Move(string sourceDirName, string destDirName)
        {
            try
            {
                System.IO.Directory.Move(sourceDirName, destDirName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
            }
        }
        /// <inheritdoc cref="IDirectory.CreateDirectory"/>
        public bool CreateDirectory(string path)
        {
            try
            {
                System.IO.Directory.CreateDirectory(path);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return false;
            }

            return true;
        }
    }
}