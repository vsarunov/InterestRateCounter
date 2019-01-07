using InterestRateCounter.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InterestRateCounter.DAL.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer> GetCustomerByIdAsync(long id);
    }
}
