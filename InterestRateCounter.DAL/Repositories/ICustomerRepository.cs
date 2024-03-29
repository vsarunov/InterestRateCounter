﻿namespace InterestRateCounter.DAL.Repositories
{
    using InterestRateCounter.Models.Models;
    using System.Threading.Tasks;

    public interface ICustomerRepository
    {
        Task<Customer> GetCustomerByIdAsync(long id);
    }
}
