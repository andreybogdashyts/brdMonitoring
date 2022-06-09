using BRD.Monitoring.Infrastructure.Settings;
using System.IO;

namespace BRD.Monitoring.Infrastructure.Watchers
{
    public class FileWatcher: IFileWatcher
    {
        private readonly ISettings _settings;
        private readonly IFileProcessor _fileProcessor;

        public FileWatcher(ISettings settings, IFileProcessor fileProcessor )
        {
            _settings = settings;
            _fileProcessor = fileProcessor;
        }

        public void Initialize(string extension)
        {
            var watcher = new FileSystemWatcher()
            {
                Path = _settings.ScanInputFolder,
                Filter = $"*{extension}",
                EnableRaisingEvents = true,
                NotifyFilter = NotifyFilters.FileName,

            };
            watcher.Created += OnCreated;
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            string ext = Path.GetExtension(e.FullPath);

            if (string.IsNullOrEmpty(ext))
            {
                return;
            }
            if (_settings.SupportedExtensions.Contains(ext.ToLower()))
            {
                _fileProcessor.Process(e.FullPath);
            }
        }

    }
}
