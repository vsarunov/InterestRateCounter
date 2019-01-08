namespace InterestRateCounter.Service.Services.Implementations
{
    using AutoMapper;
    using InterestRateCounter.DAL.Repositories;
    using InterestRateCounter.Models.Models;
    using InterestRateCounter.Service.ServiceModels;
    using System.Threading.Tasks;

    public class AgreementService : IAgreementService
    {
        private readonly IAgreementRepository _agreementRepository;
        private readonly IMapper _mapper;

        public AgreementService(IAgreementRepository agreementRepository, IMapper mapper)
        {
            _agreementRepository = agreementRepository;
            _mapper = mapper;
        }

        public async Task<AgreementModel> SaveNewAgreementAsync(AgreementModel agreement)
        {
            var mappedNewAgreement = _mapper.Map<Agreement>(agreement);

            if (mappedNewAgreement != null)
            {
                var savedAgreement = await _agreementRepository.SaveNewAgreementAsync(mappedNewAgreement);

                return _mapper.Map<AgreementModel>(savedAgreement);
            }

            return null;
        }
    }
}
