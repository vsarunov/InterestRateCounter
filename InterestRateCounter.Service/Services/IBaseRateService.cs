using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InterestRateCounter.Service.Services
{
    public interface IBaseRateService
    {
        Task<decimal?> GetBaseRateByCodeAsync(string baseRateCode);
    }
}
