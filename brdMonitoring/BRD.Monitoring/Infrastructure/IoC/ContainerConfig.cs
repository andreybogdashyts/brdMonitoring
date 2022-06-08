using Autofac;
using Autofac.Extensions.DependencyInjection;
using BRD.Monitoring.Infrastructure.Settings;
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
            return builder.Build();
        }
    }
}
