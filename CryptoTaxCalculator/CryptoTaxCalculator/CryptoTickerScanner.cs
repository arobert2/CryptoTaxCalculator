using CryptoTaxCalculator.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Timers;

namespace CryptoTaxCalculator
{
    public class CryptoTickerScanner
    {
        private CryptoTaxCalculatorContext _context;
        private HttpClient _httpClient;
        private Timer _timer;


        public CryptoTickerScanner()
        {
            DbContextOptionsBuilder options = new DbContextOptionsBuilder<CryptoTaxCalculatorContext>();
            options.UseSqlServer("(localdb)\\mssqllocaldb;Database=CryptoTaxCalculatorContext-8710fd86-0320-4410-bd1a-f36e7f9c8c48;Trusted_Connection=True;MultipleActiveResultSets=true");
            _context = new CryptoTaxCalculatorContext(options.Options as DbContextOptions<CryptoTaxCalculatorContext>);
            _httpClient = new HttpClient();
        }

        public async Task<CoinCollectionModel> ScanAsync()
        {
            HttpResponseMessage res = await _httpClient.GetAsync(@"http://coincap.io/front");

            if (!res.IsSuccessStatusCode)
                return new CoinCollectionModel();
            var val = await res.Content.ReadAsStringAsync();
            var CoinCol = JsonConvert.DeserializeObject<CoinCollectionModel>(val);
            return CoinCol;
        }

        public async void WriteData(CoinCollectionModel res)
        {
            foreach(var c in res)
            {
                var coin = c;
                coin.TimeStamp = DateTime.Now;
                _context.Add(coin);
            }

            await _context.SaveChangesAsync();
        }
    }
}
