namespace InterestRateCounter.Service.ServiceModels
{
    using System;

    public class AgreementModel
    {
        public long Id { get; set; }
        public double Amount { get; set; }
        public decimal Margin { get; set; }
        public string BaseRateCode { get; set; }
        public int Duration { get; set; }
        public long CustomerId { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
