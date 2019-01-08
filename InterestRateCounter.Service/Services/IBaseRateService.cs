namespace InterestRateCounter.Service.Services
{
    using System.Threading.Tasks;

    public interface IBaseRateService
    {
        Task<decimal?> GetBaseRateByCodeAsync(string baseRateCode);
    }
}
