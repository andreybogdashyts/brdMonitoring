using BRD.Monitoring.Infrastructure.Settings;
using System.IO;

namespace BRD.Monitoring.Infrastructure.Helpers
{
    public class DirectoryHelper : IDirectoryHelper
    {
        private readonly ISettings _settings;

        public DirectoryHelper(ISettings settings)
        {
            _settings = settings;
        }
        public void CreateMissedFolders()
        {
            if (!Directory.Exists(_settings.ScanInputFolder))
            {
                Directory.CreateDirectory(_settings.ScanInputFolder);
            }
            if (!Directory.Exists(_settings.ScanOutputFolder))
            {
                Directory.CreateDirectory(_settings.ScanOutputFolder);
            }
        }

        public string[] GetFiles()
        {
            return Directory.GetFiles(_settings.ScanInputFolder);
        }
    }
}
