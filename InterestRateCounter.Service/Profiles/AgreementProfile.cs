namespace InterestRateCounter.Service.Profiles
{
    using AutoMapper;
    using InterestRateCounter.Models.Models;
    using InterestRateCounter.Service.ServiceModels;

    public class AgreementProfile : Profile
    {
        public AgreementProfile()
        {
            CreateMap<Agreement, AgreementModel>()
                .ForMember(src => src.Id, dest => dest.MapFrom(src => src.Id))
                .ForMember(src => src.BaseRateCode, dest => dest.MapFrom(src => src.BaseRateCode))
                .ForMember(src => src.Amount, dest => dest.MapFrom(src => src.Amount))
                .ForMember(src => src.Duration, dest => dest.MapFrom(src => src.Duration))
                .ForMember(src => src.Margin, dest => dest.MapFrom(src => src.Margin))
                .ForMember(src => src.CustomerId, dest => dest.MapFrom(src => src.CustomerId))
                .ForMember(src => src.Timestamp, dest => dest.MapFrom(src => src.Timestamp))
                .ForAllOtherMembers(dest => dest.Ignore());

            CreateMap<AgreementModel, Agreement>()
                .ForMember(src => src.Id, dest => dest.MapFrom(src => src.Id))
                .ForMember(src => src.BaseRateCode, dest => dest.MapFrom(src => src.BaseRateCode))
                .ForMember(src => src.Amount, dest => dest.MapFrom(src => src.Amount))
                .ForMember(src => src.Duration, dest => dest.MapFrom(src => src.Duration))
                .ForMember(src => src.Margin, dest => dest.MapFrom(src => src.Margin))
                .ForMember(src => src.CustomerId, dest => dest.MapFrom(src => src.CustomerId))
                .ForMember(src => src.Customer, dest => dest.UseDestinationValue())
                .ForAllOtherMembers(dest => dest.Ignore());
        }
    }
}
