using System.Collections.Generic;

namespace BRD.Monitoring.Models.Output
{
    public class GenerationOutput
    {
        public GenerationOutput()
        {
            Totals = new List<Generator>();
            MaxEmissionGenerators = new List<Day>();
            ActualHateRates = new List<ActualHateRate>();
        }
        public List<Generator> Totals { get; set; }
        public List<Day> MaxEmissionGenerators { get; set; }
        public List<ActualHateRate> ActualHateRates { get; set; }
    }
}
