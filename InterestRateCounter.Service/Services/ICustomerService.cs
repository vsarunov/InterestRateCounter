using InterestRateCounter.Service.ServiceModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InterestRateCounter.Service.Services
{
    public interface ICustomerService
    {
        Task<CustomerModel> GetCustomersByIdAsync(long id);
    }
}
