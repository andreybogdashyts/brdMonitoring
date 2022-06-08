namespace BRD.Monitoring.Models
{
    public class CoalGenerator: BaseGenerator, IEmissionsRatable
    {
        public override GeneratorType Type => GeneratorType.Coal;
        public decimal TotalHeatInput { get; set; }
        public decimal ActualNetGeneration { get; set; }
        public decimal EmissionsRating { get; set; }
    }
}
