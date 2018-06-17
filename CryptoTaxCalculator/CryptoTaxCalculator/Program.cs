using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Timers;
using System.Threading.Tasks;
using CryptoTaxCalculator.Services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CryptoTaxCalculator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var cts = new CryptoTickerScanner();
            var _timer = new Timer(1000);
            Task.Run(() => { _timer.Elapsed += async (o, eargs) =>
                 {
                     var res = await cts.ScanAsync();
                     cts.WriteData(res);
                 };
            });

            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
