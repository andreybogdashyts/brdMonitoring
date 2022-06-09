using Autofac;
using Autofac.Extensions.DependencyInjection;
using BRD.Monitoring.Infrastructure.FileHandlers;
using BRD.Monitoring.Infrastructure.Helpers;
using BRD.Monitoring.Infrastructure.Settings;
using BRD.Monitoring.Infrastructure.Watchers;
using Microsoft.Extensions.DependencyInjection;

namespace BRD.Monitoring.Infrastructure.IoC
{
    public class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            var services = new ServiceCollection();
            services.AddLogging();
            
            builder.Populate(services);
            builder.RegisterType<Settings.Settings>().As<ISettings>();
            builder.RegisterType<DirectoryHelper>().As<IDirectoryHelper>();
            builder.RegisterType<CalculationHelper>().As<ICalculationHelper>();
            builder.RegisterType<FileHandlerResolver>().As<IFileHandlerResolver>();
            builder.RegisterType<FileProcessor>().As<IFileProcessor>();
            builder.RegisterType<FileWatcher>().As<IFileWatcher>();
            return builder.Build();
        }
    }
}
