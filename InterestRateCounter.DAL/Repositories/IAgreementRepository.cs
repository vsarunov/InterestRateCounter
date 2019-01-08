namespace InterestRateCounter.DAL.Repositories
{
    using InterestRateCounter.Models.Models;
    using System.Threading.Tasks;

    public interface IAgreementRepository
    {
        Task<Agreement> SaveNewAgreementAsync(Agreement agreement);
    }
}
