using System.Collections.Generic;

namespace BRD.Monitoring.Infrastructure.Settings
{
    public interface ISettings
    {
        string ScanInputFolder { get;}
        string ScanOutputFolder { get; }
        HashSet<string> SupportedExtensions { get; }
        int IterationDelay { get; }
  }
}
