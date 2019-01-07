using InterestRateCounter.Models.Context;
using InterestRateCounter.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace InterestRateCounter.DAL.Repositories.Implementations
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerContext _context;

        public CustomerRepository(CustomerContext context)
        {
            _context = context;
        }

        public async Task<Customer> GetCustomerByIdAsync(long id)
        {
            var customer = await _context.Customers
                .Where(x => x.Id == id)
                .Include(x => x.Agreements)
                .ToArrayAsync();

            return customer.FirstOrDefault();
        }
    }
}
