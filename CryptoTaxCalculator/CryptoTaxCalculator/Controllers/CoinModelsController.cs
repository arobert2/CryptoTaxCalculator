using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CryptoTaxCalculator.Models;
using CryptoTaxCalculator.Services;
using System.IO;

namespace CryptoTaxCalculator.Controllers
{
    public class CoinModelsController : Controller
    {
        private readonly CryptoTaxCalculatorContext _context;
        private readonly CoinScanService _coinScanner;

        public CoinModelsController(CryptoTaxCalculatorContext context, CoinScanService coinScanner)
        {
            _context = context;
            _coinScanner = coinScanner;
        }

        // GET: CoinModels
        public async void Index()
        {
            var scan = await _coinScanner.ScanCoinData();
            var converted = _coinScanner.JsonToCoinModel(scan);

            foreach (var c in converted)
                _context.Add(c);
            await _context.SaveChangesAsync();

        }      
    }
}
