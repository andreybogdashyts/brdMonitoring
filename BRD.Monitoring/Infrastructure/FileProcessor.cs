using BRD.Monitoring.Infrastructure.FileHandlers;
using BRD.Monitoring.Infrastructure.Settings;
using BRD.Monitoring.Models;
using BRD.Monitoring.Models.Output;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace BRD.Monitoring.Infrastructure
{
    public class FileProcessor : IFileProcessor
    {
        private readonly ISettings _settings;
        private readonly IFileHandlerResolver _handlerResolver;
        private readonly Dictionary<FactorType, Factor> _referenceData = new Dictionary<FactorType, Factor>{
        {FactorType.ValueFactor, new Factor{
            High = 0.946M,
            Medium = 0.696M,
            Low = 0.265M,
        }},
        {FactorType.EmissionsFactor, new Factor{
            High = 0.812M,
            Medium = 0.562M,
            Low = 0.312M,
        }},
    };
        public FileProcessor(ISettings settings, IFileHandlerResolver handlerResolver)
        {
            _settings = settings;
            _handlerResolver = handlerResolver;
        }
        public void Process(string path)
        {
            var ext = Path.GetExtension(path);
            var handler = _handlerResolver.Get(ext);

            var report = handler.Parse(path);

            var output = new GenerationOutput
            {
                Totals = GetTotalGenerationValue(report),
                MaxEmissionGenerators = GetEmissionDayGenerators(report),
                ActualHateRates = GetActualHateRates(report.Coal)
            };

            var fn = Path.GetFileNameWithoutExtension(path);
            handler.Save(Path.Join(_settings.ScanOutputFolder, $"{fn}_Result{ext}"), output);

            File.Delete(path);
        }

        private List<Models.Output.Day> GetEmissionDayGenerators(GenerationReport report)
        {
            var gasEmissions = new List<Models.Output.Day>();
            foreach (var g in report.Gas)
            {
                gasEmissions.AddRange(GetEmissionDays(g));
            }
            var coalEmissions = new List<Models.Output.Day>();
            foreach (var g in report.Coal)
            {
                coalEmissions.AddRange(GetEmissionDays(g));
            }
            var result = new List<Models.Output.Day>();
            result.AddRange(gasEmissions);
            result.AddRange(coalEmissions);
            return result;
        }

        private List<Models.Output.Day> GetEmissionDays<T>(T generator) where T : BaseGenerator, IEmissionsRatable 
        {
            return generator.Generation.Select(s => new Models.Output.Day
            {
                Date = s.Date,
                Name = generator.Name,
                Emission = CalculateDailyEmission(s.Energy, generator.EmissionsRating, ResolveEmissionFactor(generator.Type))
            }).ToList();
        }

        private List<Generator> GetTotalGenerationValue(GenerationReport report)
        {
            var res = new List<Generator>();
            res.AddRange(GetGenerationValue(report.Wind));
            res.AddRange(GetGenerationValue(report.Gas));
            res.AddRange(GetGenerationValue(report.Coal));
            return res;
        }

        private List<ActualHateRate> GetActualHateRates(List<CoalGenerator> coalList)
        {
            return coalList.Select(s => new ActualHateRate
            {
                Name = s.Name,
                HeatRate = CalculateActionHeatRate(s.TotalHeatInput, s.ActualNetGeneration)
            }).ToList();
        }

        private List<Generator> GetGenerationValue<T>(List<T> l) where T : BaseGenerator
        {
            return l.GroupBy(m => new { m.Name, m.Type }).Select(s => new Generator
            {
                Name = s.Key.Name,
                Total = s.Sum(t => t.Generation.Sum(i => CalculateDailyGenerationValue(i.Energy, i.Price, ResolveValueFactor(t.Type))))
            }).ToList();
        }

        private decimal CalculateDailyGenerationValue(decimal energy, decimal price, decimal valueFactor)
        {
            return energy * price * valueFactor;
        }

        private decimal CalculateDailyEmission(decimal energy, decimal emissionRating, decimal emissionFactor)
        {
            return energy * emissionRating * emissionFactor;
        }

        private decimal CalculateActionHeatRate(decimal totalHeatInput, decimal actualNetGeneration)
        {
            return actualNetGeneration != 0 ? totalHeatInput / actualNetGeneration : 0;
        }

        private decimal ResolveValueFactor(GeneratorType type)
        {
            switch (type)
            {
                case GeneratorType.OffshoreWind:
                    return _referenceData[FactorType.ValueFactor].Low;
                case GeneratorType.OnshoreWind:
                    return _referenceData[FactorType.ValueFactor].High;
                case GeneratorType.Gas:
                    return _referenceData[FactorType.ValueFactor].Medium;
                case GeneratorType.Coal:
                    return _referenceData[FactorType.ValueFactor].Medium;
            }
            throw new System.ArgumentException($"Can't resolve Value Factor. Type {type} is not supported");
        }

        private decimal ResolveEmissionFactor(GeneratorType type)
        {
            switch (type)
            {
                case GeneratorType.Gas:
                    return _referenceData[FactorType.EmissionsFactor].Medium;
                case GeneratorType.Coal:
                    return _referenceData[FactorType.ValueFactor].High;
            }
            throw new System.ArgumentException($"Can't resolve Emission Factor. Type {type} is not supported");
        }
    }
}
