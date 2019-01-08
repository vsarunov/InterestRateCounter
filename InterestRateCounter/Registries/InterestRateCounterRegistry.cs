namespace InterestRateCounter.Registries
{
    using AutoMapper;
    using StructureMap;

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
