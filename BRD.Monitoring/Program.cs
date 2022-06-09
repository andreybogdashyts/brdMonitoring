using Autofac;
using BRD.Monitoring.Infrastructure;
using BRD.Monitoring.Infrastructure.Helpers;
using BRD.Monitoring.Infrastructure.IoC;
using BRD.Monitoring.Infrastructure.Settings;
using BRD.Monitoring.Infrastructure.Watchers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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

                    foreach (var ext in settings.SupportedExtensions)
                    {
                        var watcher = scope.Resolve<IFileWatcher>();
                        watcher.Initialize(ext);
                    }
                    new AutoResetEvent(false).WaitOne();
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

