using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InterestRateCounter.DAL.ApiService
{
    public interface IApiService
    {
        Task<string> GetData(string uri);
    }
}
