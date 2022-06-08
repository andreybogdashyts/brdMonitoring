using System.Collections.Generic;

namespace BRD.Monitoring.Models
{
  public class WindGenerator : BaseGenerator
  {
    public override GeneratorType Type
    {
      get
      {
        switch (Location)
        {
          case "Onshore":
            return GeneratorType.OnshoreWind;
          case "Offshore":
            return GeneratorType.OffshoreWind;
        }
        throw new System.Exception($"Location {Location} is not suported");
      }
    }
    public string Location { get; set; }
  }
}
