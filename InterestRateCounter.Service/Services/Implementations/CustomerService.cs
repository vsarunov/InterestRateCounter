using AutoMapper;
using InterestRateCounter.DAL.Repositories;
using InterestRateCounter.Service.ServiceModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InterestRateCounter.Service.Services.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CustomerModel> GetCustomersByIdAsync(long id)
        {
            var customers = await _repository.GetCustomerByIdAsync(id);
            return _mapper.Map<CustomerModel>(customers);
        }
    }
}
