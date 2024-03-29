﻿namespace InterestRateCounter.Service.Services
{
    using InterestRateCounter.Service.ServiceModels;
    using System.Threading.Tasks;


    public interface ICustomerService
    {
        Task<CustomerModel> GetCustomersByIdAsync(long id);
    }
}
