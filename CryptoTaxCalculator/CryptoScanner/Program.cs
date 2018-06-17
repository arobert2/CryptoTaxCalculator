using Microsoft.EntityFrameworkCore;
using System;
using System.Timers;

namespace CryptoScanner
{
    class Program
    {
        static void Main(string[] args)
        {
            Timer timer = new Timer(5000);
            CoinScanService coinscanner = new CoinScanService();

            timer.Elapsed += async (o, args2) => {
                var coindata = await coinscanner.ScanCoinData();
                CoinBuffer.PushCoinCollection(coindata);           
                Console.WriteLine("Scan complete at {0}", DateTime.Now);
            };
            timer.Start();
            while (true) ;
        }
    }
}
