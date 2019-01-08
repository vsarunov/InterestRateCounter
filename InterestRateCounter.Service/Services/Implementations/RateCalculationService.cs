namespace InterestRateCounter.Service.Services.Implementations
{
    using InterestRateCounter.Service.ServiceModels;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class RateCalculationService : IRateCalculationService
    {
        private readonly ICustomerService _personService;
        private readonly IBaseRateService _baseRateService;
        private readonly IAgreementService _agreementService;
        private readonly ILogger<RateCalculationService> _log;

        public RateCalculationService(ICustomerService personService, IBaseRateService baseRateService, IAgreementService agreementService, ILogger<RateCalculationService> log)
        {
            _personService = personService;
            _baseRateService = baseRateService;
            _agreementService = agreementService;
            _log = log;
        }

        public async Task<ResultModel> GetCustomerDataAsync(AgreementModel agreementModel)
        {
            _log.LogInformation("Starting interest rate data gathering and calculations");

            var customer = await _personService.GetCustomersByIdAsync(agreementModel.CustomerId);

            if (customer == null)
            {
                _log.LogError($"No Customer found with id: {agreementModel.CustomerId}");
                return null;
            }

            ResultModel result = new ResultModel();
            result.Customer = customer;
            result.InterestRates.NewInterestRate = await CalculateNewInterestRate(agreementModel.BaseRateCode, agreementModel.Margin);

            var latestAgreement = customer.Agreements?.OrderByDescending(x => x.Timestamp).FirstOrDefault();

            if (latestAgreement != null)
            {
                result.InterestRates.CurrentInterestRate = await GetCurrentInterestRate(latestAgreement);

                result.InterestRates.InterestRateDifference = CalculateRateDifference(result.InterestRates.CurrentInterestRate, result.InterestRates.NewInterestRate);
            }

            result.Agreement = await SaveNewAgreementAsync(agreementModel);

            return result;
        }

        private async Task<AgreementModel> SaveNewAgreementAsync(AgreementModel agreementModel)
        {
            agreementModel.Timestamp = DateTime.Now;
            return await _agreementService.SaveNewAgreementAsync(agreementModel);
        }

        private decimal? CalculateRateDifference(decimal? currentRate, decimal? newRate)
        {
            if (currentRate.HasValue && newRate.HasValue)
            {
                return Math.Abs(currentRate.Value - newRate.Value);
            }
            return null;
        }

        private async Task<decimal?> CalculateNewInterestRate(string baseRateCode, decimal margin)
        {
            var baseRate = await _baseRateService.GetBaseRateByCodeAsync(baseRateCode);

            if (baseRate.HasValue)
            {
                return baseRate.Value + margin;
            }

            return null;
        }

        private async Task<decimal?> GetCurrentInterestRate(AgreementModel latestAgreement)
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
