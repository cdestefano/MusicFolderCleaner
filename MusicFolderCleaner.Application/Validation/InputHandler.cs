using System.ComponentModel.Design;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using MusicFolderCleaner.Application.Directory;

namespace MusicFolderCleaner.Application.Validation
{
    /// <inheritdoc cref="IInputHandler"/>
    public class InputHandler : IInputHandler
    {
        private readonly IDirectory _directory;
        private readonly IErrorLogger _errorLogger;

        /// <summary>
        /// Creates a new instance of <see cref="InputHandler"/>.
        /// </summary>
        /// <param name="errorLogger">The logging library to be used for errors.</param>
        /// <param name="directory">The directory class for basic directory modification.</param>
        public InputHandler(IErrorLogger errorLogger, IDirectory directory)
        {
            _errorLogger = errorLogger;
            _directory = directory;
        }

        /// <inheritdoc cref="IInputHandler.Validate"/>
        public bool Validate(string[] args)
        {
            args = TrimInput(args);

            if (args.Any(x => x.Contains("-help")) || (args.Length == 1 && args[0].Contains("?")))
            {
                _errorLogger.LogError(HelpCommand());
                return false;
            }

            if (args.Length != 3)
            {
                _errorLogger.LogError(InvalidNumberOfArguments());
                return false;
            }

            if (!_directory.Exists(args[0]))
            {
                _errorLogger.LogError(NotADirectory("first"));
                return false;
            }

            if (!_directory.Exists(args[1]) && !MakeDestinationDirectoryIfItDoesNotExist(args[1]))
            {
                _errorLogger.LogError(NotADirectory("second"));
                return false;
            }

            // I'm not validating file extensions, so you must add the full extension with a period. This will only check for a period.
            if (!args[2].Contains("."))
            {
                _errorLogger.LogError(NotAFileExtension());
                return false;
            }

            return true;
        }

        private string[] TrimInput(string[] args)
        {
            var charactersToTrim = new[] {' ', '\''};
            for(var i = 0; i < args.Length; i++)
            {
                args[i] = args[i].Trim(charactersToTrim);
            }

            return args;
        }

        private static string HelpCommand()
        {
            return
                "To run the MusicFolderCleaner application, you need to provide two arguments. The first, the full or relative path to a directory and the second, the file extension to look for.  All directories that do not contain a file for this extension will be deleted.";
        }

        private static string InvalidNumberOfArguments()
        {
            return "Invalid Number of Arguments entered.  Type '-help' for more information";
        }

        private static string NotADirectory(string position)
        {
            return $"The {position} input parameter is not a directory";
        }

        private bool MakeDestinationDirectoryIfItDoesNotExist(string destinationPath)
        {
            return _directory.CreateDirectory(destinationPath);
        }

        private static string NotAFileExtension()
        {
            return "The third input parameter is not a file extension";
        }
    }
}