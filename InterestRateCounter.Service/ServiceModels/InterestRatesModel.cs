namespace InterestRateCounter.Service.ServiceModels
{
    public class InterestRatesModel
    {
        public decimal? NewInterestRate { get; set; }
        public decimal? CurrentInterestRate { get; set; }
        public decimal? InterestRateDifference { get; set; }
    }
}
