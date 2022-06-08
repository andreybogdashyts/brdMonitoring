using Autofac;
using BRD.Monitoring.Infrastructure;
using BRD.Monitoring.Infrastructure.IoC;
using BRD.Monitoring.Infrastructure.Settings;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;

namespace BRD.Monitoring
{
    internal class Program
    {
        static int Main(string[] args)
        {
            var processResult = ProcessResultType.Success;
            Console.WriteLine("Monitoring tool has started");
            Console.WriteLine("Press ESC to exit");
            var container = ContainerConfig.Configure();
            using (var scope = container.BeginLifetimeScope())
            {
                try
                {
                    while (Console.ReadKey(true).Key != ConsoleKey.Escape)
                    {
                        var settings = scope.Resolve<ISettings>();

                        var files = Directory.GetFiles(settings.ScanFolder);
                        if (files.Any(m => Path.GetExtension(m) == ".xml"))
                        {

                        }

                    }
                }
                catch (Exception ex)
                {
                    var logger = scope.Resolve<ILogger<Program>>();
                    var error = $"Process has stopped with error: {ex}";
                    logger.LogError(error);
                    Console.Error.WriteLine(error);
                    processResult = ProcessResultType.Fail;
                }
            }
            return (int)processResult;
        }
    }
}
