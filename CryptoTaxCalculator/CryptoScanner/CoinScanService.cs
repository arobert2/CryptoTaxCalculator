using CryptoScanner.Model;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CryptoScanner
{
    public class CoinScanService
    {
        private HttpClient _client = new HttpClient();

        public CoinScanService()
        {
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<CoinCollection> ScanCoinData()
        {
            HttpResponseMessage res = await _client.GetAsync(@"http://coincap.io/front");
            var time = DateTime.Now;

            if (!res.IsSuccessStatusCode)
                return null;

            var val = await res.Content.ReadAsStringAsync();
            var coincollection = JsonToCoinModel(val, time);

            return coincollection;
        }

        public CoinCollection JsonToCoinModel(string res, DateTime time)
        {
            if (res == string.Empty)
                return new CoinCollection();

            var coincollection = JsonConvert.DeserializeObject<CoinCollection>(res);

            foreach (var c in coincollection)
                c.TimeStamp = time;

            return coincollection;
        }
    }
}
