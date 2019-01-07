using AutoMapper;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterestRateCounter.Registries
{
    public class InterestRateCounterRegistry: Registry
    {
        public InterestRateCounterRegistry()
        {
            Scan(_ =>
            {
                _.Assembly("InterestRateCounter.Common");
                _.Assembly("InterestRateCounter.DAL");
                _.Assembly("InterestRateCounter.Models");
                _.Assembly("InterestRateCounter.Service");
                _.AddAllTypesOf(typeof(Profile));
                _.WithDefaultConventions();
                _.LookForRegistries();

            });
        }
    }
}
