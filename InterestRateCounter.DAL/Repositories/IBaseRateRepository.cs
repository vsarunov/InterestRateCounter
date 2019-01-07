using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InterestRateCounter.DAL.Repositories
{
    public interface IBaseRateRepository
    {
        Task<string> GetBaseRate(string baseRateCode);
    }
}
