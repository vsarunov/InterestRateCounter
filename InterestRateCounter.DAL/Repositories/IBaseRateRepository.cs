namespace InterestRateCounter.DAL.Repositories
{
    using System.Threading.Tasks;

    public interface IBaseRateRepository
    {
        Task<string> GetBaseRate(string baseRateCode);
    }
}
