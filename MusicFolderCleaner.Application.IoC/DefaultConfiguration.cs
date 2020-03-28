using Autofac;
using MusicFolderCleaner.Application.Directory;
using MusicFolderCleaner.Application.Validation;

namespace MusicFolderCleaner.Application.IoC
{
    public class DefaultConfiguration
    {
        public IContainer Container { get; set; }

        public DefaultConfiguration()
        {
            var builder = new ContainerBuilder();

            RegisterComponents(builder);

            Container = builder.Build();
        }

        private void RegisterComponents(ContainerBuilder builder)
        {
            builder.RegisterType<Directory.Directory>().As<IDirectory>();
            builder.RegisterType<DirectoryValidator>().As<IDirectoryValidator>();
            builder.RegisterType<DirectoryProcessor>().As<IDirectoryProcessor>();
            builder.RegisterType<InputHandler>().As<IInputHandler>();

            // Implement a real logger if you want to redirect errors outside of the Console.
            builder.RegisterType<ErrorLogger>().As<IErrorLogger>();
        }
    }
}