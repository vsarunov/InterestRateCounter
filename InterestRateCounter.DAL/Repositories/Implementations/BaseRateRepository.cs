using InterestRateCounter.DAL.ApiService;
using InterestRateCounter.Models.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InterestRateCounter.DAL.Repositories.Implementations
{
    public class BaseRateRepository : IBaseRateRepository
    {
        private readonly Uri _vilibidUri;
        private readonly IApiService _apiService;

        public BaseRateRepository(IOptions<AppSettings> appSettings, IApiService apiService)
        {
            _vilibidUri = appSettings.Value.VilibidUri;
            _apiService = apiService;
        }

        public async Task<string> GetBaseRate(string baseRateCode)
        {
            string url = $"{_vilibidUri}/webservices/VilibidVilibor/VilibidVilibor.asmx/getLatestVilibRate?RateType={baseRateCode}";

            return await _apiService.GetData(url);
        }
    }
}
