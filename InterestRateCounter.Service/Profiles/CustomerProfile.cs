namespace InterestRateCounter.Service.Profiles
{
    using AutoMapper;
    using InterestRateCounter.Models.Models;
    using InterestRateCounter.Service.ServiceModels;

    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerModel>()
             .ForMember(src => src.Id, dest => dest.MapFrom(src => src.Id))
             .ForMember(src => src.FullName, dest => dest.MapFrom(src => src.FullName))
             .ForMember(src => src.Agreements, dest => dest.MapFrom(src => src.Agreements))
             .ForAllOtherMembers(dest => dest.Ignore());
        }
    }
}
