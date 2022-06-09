using BRD.Monitoring.Infrastructure.FileHandlers;
using BRD.Monitoring.Infrastructure.Helpers;
using BRD.Monitoring.Infrastructure.Settings;
using System.IO;

namespace BRD.Monitoring.Infrastructure
{
    public class FileProcessor : IFileProcessor
    {
        private readonly ISettings _settings;
        private readonly IFileHandlerResolver _handlerResolver;
        private readonly ICalculationHelper _calculationHelper;
        private readonly IDirectoryHelper _directoryHelper;

        public FileProcessor(ISettings settings, 
            IFileHandlerResolver handlerResolver,
            ICalculationHelper calculationHelper,
            IDirectoryHelper directoryHelper)
        {
            _settings = settings;
            _handlerResolver = handlerResolver;
            _calculationHelper = calculationHelper;
            _directoryHelper = directoryHelper;
        }
        public void Process(string path)
        {
            var ext = Path.GetExtension(path);
            var handler = _handlerResolver.Get(ext);

            var report = handler.Parse(path);
            var output = _calculationHelper.Calculate(report);
            var fn = Path.GetFileNameWithoutExtension(path);
            handler.Save(Path.Join(_settings.ScanOutputFolder, $"{fn}_Result{ext}"), output);

            _directoryHelper.DeleteFile(path);
        }   
    }
}
