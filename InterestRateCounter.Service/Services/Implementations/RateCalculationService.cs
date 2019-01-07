using InterestRateCounter.Service.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterestRateCounter.Service.Services.Implementations
{
    public class RateCalculationService : IRateCalculationService
    {
        private readonly ICustomerService _personService;
        private readonly IBaseRateService _baseRateService;
        public RateCalculationService(ICustomerService personService, IBaseRateService baseRateService)
        {
            _personService = personService;
            _baseRateService = baseRateService;
        }

        public async Task<ResultModel> GetCustomerDataAsync(AgreementModel agreementModel)
        {
            var customerTask = _personService.GetCustomersByIdAsync(agreementModel.CustomerId);
            var baseRateTask = _baseRateService.GetBaseRateByCodeAsync(agreementModel.BaseRateCode);

            Task.WaitAll(customerTask, baseRateTask);

            var customer = customerTask.Result;
            var baseRate = baseRateTask.Result;

            if (customer == null) return null;

            var latestAgreement = customer.Agreements.OrderBy(x => x.Timestamp).FirstOrDefault();

            ResultModel result = new ResultModel();
            if (latestAgreement != null)
            {
                result.InterestRates.CurrentInterestRate = await GetCurrentInterestRate(latestAgreement);
            }

            return 5;
        }

        public async Task<decimal?> GetCurrentInterestRate(AgreementModel latestAgreement)
        {
            var baseRate = await _baseRateService.GetBaseRateByCodeAsync(latestAgreement.BaseRateCode);

            if (baseRate.HasValue)
            {
                return baseRate.Value + latestAgreement.Margin;
            }

            return null;
        }

    }
}
