using System.Collections.Generic;

namespace BRD.Monitoring.Models
{
    public class GenerationReport
    {
        public List<WindGenerator> Wind { get; set; }
        public List<GasGenerator> Gas { get; set; }
        public List<CoalGenerator> Coal { get; set; }
    }
}
