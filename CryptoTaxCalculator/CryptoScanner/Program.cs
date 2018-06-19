using CryptoScanner.DbSystem;
using System;
using System.Threading.Tasks;
using System.Timers;

namespace CryptoScanner
{
    class Program
    {
        static void Main(string[] args)
        {
            Timer timer = new Timer(5000);
            CoinScanService coinscanner = new CoinScanService();
            //CoinDbController dbController = new CoinDbController("Driver={SQL Server};Server=(localdb)\\mssqllocaldb;Database=CoinHistories;");
            CoinDbController dbController = new CoinDbController("Driver={SQL Server};Server=(localdb)\\MSSQLLocalDB;Trusted_Connection=Yes;Database=CoinHistories;");
            
            BufferDbWriter bufferWriter = new BufferDbWriter();

            timer.Elapsed += async (o, args2) => {
                var coindata = await coinscanner.ScanCoinData();
                CoinBuffer.PushCoinCollection(coindata);           
                Console.WriteLine("Scan complete at {0}", DateTime.Now);
            };

            Task.Run(() => bufferWriter.StartBufferWrite(dbController));

            timer.Start();
            while (true) ;
        }
    }
}
