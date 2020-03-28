namespace MusicFolderCleaner.Application.Directory
{
    /// <summary>
    /// Processes the files that are going to be deleted.
    /// </summary>
    public interface IDirectoryProcessor
    {
        /// <summary>
        ///  Looks at all directories from the given directory and deletes the directories that don't correspond to the file
        ///  Extension.
        /// </summary>
        /// <param name="fullFilePath">The starting top directory.</param>
        /// <param name="fullDestinationPath">the destination for the files that don't match.</param>
        /// <param name="fileExtension">The file extension that is going to be deleted.</param>
        void Process(string fullFilePath, string fullDestinationPath, string fileExtension);
    }
}