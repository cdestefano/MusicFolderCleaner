namespace MusicFolderCleaner.Application.Validation
{
    /// <summary>
    /// Validates that the given input is valid and the application can start.
    /// </summary>
    public interface IInputHandler
    {
        /// <summary>
        /// Validates if the input arguments are valid.
        /// </summary>
        /// <param name="args">The input parameters from the cmd line.</param>
        /// <returns>If the application can start.</returns>
        bool Validate(string[] args);
    }
}