namespace InterestRateCounter.Service.Services.Implementations
{
    using AutoMapper;
    using InterestRateCounter.DAL.Repositories;
    using InterestRateCounter.Service.ServiceModels;
    using Microsoft.Extensions.Logging;
    using System.Threading.Tasks;

    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomerService> _log;
        public CustomerService(ICustomerRepository repository, ILogger<CustomerService> log, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _log = log;
        }

        public async Task<CustomerModel> GetCustomersByIdAsync(long id)
        {
            _log.LogInformation($"Getting customer by id: {id}");
            var customers = await _repository.GetCustomerByIdAsync(id);
            return _mapper.Map<CustomerModel>(customers);
        }
    }
}
