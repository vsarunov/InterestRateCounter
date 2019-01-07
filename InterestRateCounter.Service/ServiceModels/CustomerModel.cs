namespace InterestRateCounter.Service.ServiceModels
{
    using System.Collections.Generic;

    public class CustomerModel
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public List<AgreementModel> Agreements { get; set; }
    }
}
