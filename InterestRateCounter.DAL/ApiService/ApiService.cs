using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Linq;

namespace InterestRateCounter.DAL.ApiService
{
    public class ApiService : IApiService
    {
        public async Task<string> GetData(string uri)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync(uri);
                    response.EnsureSuccessStatusCode();

                    var stringResult = await response.Content.ReadAsStringAsync();

                    XDocument doc = XDocument.Parse(stringResult);

                    var result = doc.Elements().FirstOrDefault()?.Value;

                    return result;
                }
                catch (Exception e)
                {
                    return string.Empty;
                }
            }
        }
    }
}
