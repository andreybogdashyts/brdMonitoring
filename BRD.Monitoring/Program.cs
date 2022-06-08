using Autofac;
using BRD.Monitoring.Infrastructure;
using BRD.Monitoring.Infrastructure.Helpers;
using BRD.Monitoring.Infrastructure.IoC;
using BRD.Monitoring.Infrastructure.Settings;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace BRD.Monitoring
{
    internal class Program
    {
        static int Main(string[] args)
        {
            var processResult = ProcessResultType.Success;
            Console.WriteLine("Monitoring tool has started");
            var container = ContainerConfig.Configure();
            using (var scope = container.BeginLifetimeScope())
            {
                try
                {
                    var settings = scope.Resolve<ISettings>();
                    scope.Resolve<IDirectoryHelper>().CreateMissedFolders();

                    while (true)
                    {
                        var files = Directory.GetFiles(settings.ScanInputFolder);
                        foreach (var f in files)
                        {
                            var ext = Path.GetExtension(f);
                            if (string.IsNullOrEmpty(ext))
                            {
                                continue;
                            }
                            if (files.Any(m => settings.SupportedExtensions.Contains(ext.ToLower())))
                            {
                                scope.Resolve<IFileProcessor>().Process(f);
                            }
                        }
                        Thread.Sleep(settings.IterationDelay);
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

