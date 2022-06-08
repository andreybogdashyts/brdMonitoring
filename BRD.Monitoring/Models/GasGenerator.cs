namespace BRD.Monitoring.Models
{
  public class GasGenerator : BaseGenerator, IEmissionsRatable
    {
    public override GeneratorType Type => GeneratorType.Gas;
    public decimal EmissionsRating { get; set; }
  }
}
