namespace InterestRateCounter.DAL.Repositories.Implementations
{
    using InterestRateCounter.Models.Context;
    using InterestRateCounter.Models.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;
    using System.Linq;
    using Microsoft.Extensions.Logging;

    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerContext _context;
        private readonly ILogger<CustomerRepository> _log;
        public CustomerRepository(CustomerContext context, ILogger<CustomerRepository> log)
        {
            _context = context;
            _log = log;
        }

        public async Task<Customer> GetCustomerByIdAsync(long id)
        {
            _log.LogInformation($"Getting customer by id: {id}");

            var customer = await _context.Customers
                .Where(x => x.Id == id)
                .Include(x => x.Agreements)
                .ToArrayAsync();

            return customer?.FirstOrDefault();
        }
    }
}
