using System.IO;

namespace MusicFolderCleaner.Application.Directory
{
    /// <summary>
    /// A wrapper for testing of <see cref="System.IO.Directory" />
    /// </summary>
    public interface IDirectory
    {
        /// <inheritdoc cref="System.IO.Directory.GetFiles(string, string, System.IO.SearchOption)" />
        string[] GetFiles(string path, string searchPattern, SearchOption searchOption);

        /// <inheritdoc cref="System.IO.Directory.GetDirectories(string)" />
        string[] GetDirectories(string path);

        /// <inheritdoc cref="System.IO.Directory.Exists(string)" />
        bool Exists(string path);

        /// <inheritdoc cref="System.IO.Directory.Move(string, string)" />
        void Move(string sourceDirName, string destDirName);

        /// <summary>
        /// Creates a new directory. Instead of returning DirectoryInfo (violating Liskov substitution principle) this will
        /// return true or false and the end user can correct an exceptions that come from this call.
        /// </summary>
        /// <param name="path">The destination of the new directory to be created.</param>
        /// <returns>If the creation was successful.</returns>
        bool CreateDirectory(string path);
    }
}