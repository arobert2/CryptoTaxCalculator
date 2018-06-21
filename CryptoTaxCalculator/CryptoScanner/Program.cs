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
            CoinDbController dbController = new CoinDbController(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CoinHistories;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"); 

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
