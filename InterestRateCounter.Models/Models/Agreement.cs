using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InterestRateCounter.Models.Models
{
    public class Agreement
    {
        public long Id { get; set; }
        public double Amount { get; set; }
        public decimal Margin { get; set; }
        public string BaseRateCode { get; set; }
        public int Duration { get; set; }
        public long CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
