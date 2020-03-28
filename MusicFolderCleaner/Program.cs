using Autofac;
using MusicFolderCleaner.Application.Directory;
using MusicFolderCleaner.Application.IoC;
using MusicFolderCleaner.Application.Validation;

namespace MusicFolderCleaner
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var defaultConfiguration = new DefaultConfiguration();

            using (var scope = defaultConfiguration.Container.BeginLifetimeScope())
            {
                var inputValidator = scope.Resolve<IInputHandler>();
                if (inputValidator.Validate(args))
                {
                    var directoryProcessor = scope.Resolve<IDirectoryProcessor>();

                    var sourceDirectory = args[0];
                    var destinationDirectory = args[1];
                    var fileExtension = args[2];

                    directoryProcessor.Process(sourceDirectory, destinationDirectory, fileExtension);
                }
            }
        }
    }
}