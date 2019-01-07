namespace InterestRateCounter.Models.Models
{
    using System.Collections.Generic;

    public class Customer
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public List<Agreement> Agreements { get; set; }
    }
}
