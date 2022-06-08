using System.Configuration;

namespace BRD.Monitoring.Infrastructure.Settings
{
    public class Settings : ISettings
    {
        public string ScanFolder => ConfigurationManager.AppSettings["ScanFolder"];
    }
}
