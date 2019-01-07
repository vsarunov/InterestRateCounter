namespace InterestRateCounter.Service.Services
{
    using InterestRateCounter.Service.ServiceModels;
    using System.Threading.Tasks;

    public interface IRateCalculationService
    {
        Task<ResultModel> GetCustomerDataAsync(AgreementModel agreementModel);
    }
}
