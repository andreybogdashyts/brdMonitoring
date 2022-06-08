using System;
using System.Collections.Generic;

namespace BRD.Monitoring.Models
{
    public abstract class BaseGenerator
    {
        public string Name { get; set; }
        public List<Day> Generation { get; set; }
        public virtual GeneratorType Type { get; }
  }
}
