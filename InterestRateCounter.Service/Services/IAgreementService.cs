namespace InterestRateCounter.Service.Services
{
    using InterestRateCounter.Service.ServiceModels;
    using System.Threading.Tasks;


    public interface IAgreementService
    {
        Task<AgreementModel> SaveNewAgreementAsync(AgreementModel agreement);
    }
}
