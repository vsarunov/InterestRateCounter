using System;
using System.Collections.Generic;
using System.Text;

namespace InterestRateCounter.Service.ServiceModels
{
    public class ResultModel
    {
        public CustomerModel Customer { get; set; }
        public AgreementModel Agreement { get; set; }
        public InterestRatesModel InterestRates { get; set; } = new InterestRatesModel()
    }
}
