using InterestRateCounter.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InterestRateCounter.Service.Services.Implementations
{
    public class BaseRateService : IBaseRateService
    {
        private readonly IBaseRateRepository _baseRateRepository;
        public BaseRateService(IBaseRateRepository baseRateRepository)
        {
            _baseRateRepository = baseRateRepository;
        }

        public async Task<decimal?> GetBaseRateByCodeAsync(string baseRateCode)
        {
            var resultString = await _baseRateRepository.GetBaseRate(baseRateCode);

            decimal result;
            if (decimal.TryParse(resultString, out result))
            {
                return result;
            }

            return null;
        }
    }
}
