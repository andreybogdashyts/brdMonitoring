using System.Collections.Generic;
using System.Configuration;

namespace BRD.Monitoring.Infrastructure.Settings
{
  public class Settings : ISettings
  {
    public string ScanInputFolder => ConfigurationManager.AppSettings["ScanInputFolder"];
    public string ScanOutputFolder => ConfigurationManager.AppSettings["ScanOutputFolder"];
    public HashSet<string> SupportedExtensions => new HashSet<string>() { ".xml" };
    public int IterationDelay => 100;
  }
}
