using BRD.Monitoring.Infrastructure.Helpers;
using BRD.Monitoring.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BRD.Monitoring.Tests.Infrastructure.Helpers
{
    [TestClass]
    public class CalculationHelperTests
    {
        #region test generator initialization

        private GenerationReport _generationReport = new GenerationReport
        {
            Wind = new List<WindGenerator>
            {
                new WindGenerator
                {
                    Generation = new List<Day>
                    {
                        new Day
                        {
                            Date = new System.DateTime(2017, 01, 01),
                            Energy = 56.578M,
                            Price = 29.542M,
                        },
                        new Day
                        {
                            Date = new System.DateTime(2017, 02, 01),
                            Energy = 48.540M,
                            Price = 22.954M,
                        },
                        new Day
                        {
                            Date = new System.DateTime(2017, 01, 01),
                            Energy = 98.167M,
                            Price = 24.059M,
                        }
                    },
                    Location = "Onshore"
                },
                new WindGenerator
                {
                    Generation = new List<Day>
                    {
                        new Day
                        {
                            Date = new System.DateTime(2017, 01, 01),
                            Energy = 56.578M,
                            Price = 29.542M,
                        },
                        new Day
                        {
                            Date = new System.DateTime(2017, 02, 01),
                            Energy = 48.540M,
                            Price = 22.954M,
                        },
                        new Day
                        {
                            Date = new System.DateTime(2017, 01, 01),
                            Energy = 98.167M,
                            Price = 24.059M,
                        }
                    },
                    Location = "Offshore"
                }
            },
            Gas = new List<GasGenerator>
            {
                new GasGenerator
                {
                    Generation = new List<Day>
                    {
                        new Day
                        {
                            Date = new System.DateTime(2017, 01, 01),
                            Energy = 259.235M,
                            Price = 15.837M,
                        },
                        new Day
                        {
                            Date = new System.DateTime(2017, 02, 01),
                            Energy = 235.975M,
                            Price = 16.556M,
                        },
                        new Day
                        {
                            Date = new System.DateTime(2017, 01, 01),
                            Energy = 240.325M,
                            Price = 17.551M,
                        }
                    },
                    EmissionsRating = 0.038M
                }
            },            
            Coal = new List<CoalGenerator>
            {
                new CoalGenerator
                {
                    Generation = new List<Day>
                    {
                        new Day
                        {
                            Date = new System.DateTime(2017, 01, 01),
                            Energy = 350.487M,
                            Price = 10.146M,
                        },
                        new Day
                        {
                            Date = new System.DateTime(2017, 02, 01),
                            Energy = 348.611M,
                            Price = 11.815M,
                        },
                        new Day
                        {
                            Date = new System.DateTime(2017, 03, 01),
                            Energy = 98.167M,
                            Price = 11.815M,
                        }
                    },
                    EmissionsRating = 0M,
                    TotalHeatInput = 11.815M,
                    ActualNetGeneration = 11.815M,
                }
            }
        };

        #endregion

        [TestMethod]
        public void TestCalculate_Success()
        {
            var ch = new CalculationHelper();
            var r = ch.Calculate(_generationReport);

            Assert.IsNotNull(r);
            Assert.AreEqual(r.ActualHateRates.Count, 1);
            Assert.AreEqual(r.MaxEmissionGenerators.Count, 6);
            Assert.AreEqual(r.Totals.Count, 4);
            Assert.AreEqual(r.ActualHateRates[0].HeatRate, 1);
            Assert.AreEqual(r.MaxEmissionGenerators[0].Emission, 5.536222660M);
            //....
        }
    }
}
