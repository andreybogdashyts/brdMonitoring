using BRD.Monitoring.Models;
using BRD.Monitoring.Models.Output;

namespace BRD.Monitoring.Infrastructure.Helpers
{
    public interface ICalculationHelper
    {
        GenerationOutput Calculate(GenerationReport report);
    }
}
