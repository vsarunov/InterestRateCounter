namespace InterestRateCounter.Service.Services.Implementations
{
    using InterestRateCounter.DAL.Repositories;
    using Microsoft.Extensions.Logging;
    using System.Threading.Tasks;

    public class BaseRateService : IBaseRateService
    {
        private readonly IBaseRateRepository _baseRateRepository;
        private readonly ILogger<BaseRateService> _log;
        public BaseRateService(IBaseRateRepository baseRateRepository, ILogger<BaseRateService> log)
        {
            _baseRateRepository = baseRateRepository;
            _log = log;
        }

        public async Task<decimal?> GetBaseRateByCodeAsync(string baseRateCode)
        {
            _log.LogInformation($"Starting getting base rate value for base rate code: {baseRateCode}");
            var resultString = await _baseRateRepository.GetBaseRate(baseRateCode);

            decimal result;
            if (decimal.TryParse(resultString, out result))
            {
                return result;
            }

            _log.LogError($"Failed to get base rate value for base rate code: {baseRateCode}");
            return null;
        }
    }
}
