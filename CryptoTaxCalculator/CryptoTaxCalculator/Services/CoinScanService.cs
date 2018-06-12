using CryptoTaxCalculator.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Timers;

namespace CryptoTaxCalculator.Services
{
    public class CoinScanService
    {
        private readonly CryptoTaxCalculatorContext _context;
        private HttpClient _client = new HttpClient();

        public CoinScanService(CryptoTaxCalculatorContext context)
        {
            _context = context;
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<string> ScanCoinData()
        {
            HttpResponseMessage res = await _client.GetAsync(@"http://coincap.io/front");
            
            if (!res.IsSuccessStatusCode)
                return null;
            var val = await res.Content.ReadAsStringAsync();
            return val;
        }

        public CoinCollectionModel JsonToCoinModel(string res)
        {
            if (res == string.Empty)
                return new CoinCollectionModel();
            System.Diagnostics.Debug.WriteLine(res);
            return JsonConvert.DeserializeObject<CoinCollectionModel>(res);
        }
    }
}
