namespace InterestRateCounter.DAL.Repositories.Implementations
{
    using InterestRateCounter.DAL.ApiService;
    using InterestRateCounter.Models.Settings;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using System;
    using System.Threading.Tasks;

    public class BaseRateRepository : IBaseRateRepository
    {
        private readonly Uri _vilibidUri;
        private readonly IApiService _apiService;
        private readonly ILogger<BaseRateRepository> _log;

        public BaseRateRepository(IOptions<AppSettings> appSettings, IApiService apiService, ILogger<BaseRateRepository> log)
        {
            _vilibidUri = appSettings.Value.VilibidUri;
            _apiService = apiService;
            _log = log;
        }

        public async Task<string> GetBaseRate(string baseRateCode)
        {
            _log.LogInformation($"Starting getting base rate value for base rate code: {baseRateCode}");

            string url = $"{_vilibidUri}/webservices/VilibidVilibor/VilibidVilibor.asmx/getLatestVilibRate?RateType={baseRateCode}";

            return await _apiService.GetData(url);
        }
    }
}
