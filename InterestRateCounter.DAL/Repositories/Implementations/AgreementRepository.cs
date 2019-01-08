namespace InterestRateCounter.DAL.Repositories.Implementations
{
    using InterestRateCounter.Models.Context;
    using InterestRateCounter.Models.Models;
    using Microsoft.Extensions.Logging;
    using System.Threading.Tasks;

    public class AgreementRepository : IAgreementRepository
    {
        private readonly CustomerContext _context;
        private readonly ILogger<AgreementRepository> _log;
        public AgreementRepository(CustomerContext context, ILogger<AgreementRepository> log)
        {
            _context = context;
            _log = log;
        }

        public async Task<Agreement> SaveNewAgreementAsync(Agreement agreement)
        {
            _log.LogInformation("Starting saving new agreement");

            var result = await _context.Agreements.AddAsync(agreement);

            await _context.SaveChangesAsync();

            return result.Entity;
        }
    }
}
