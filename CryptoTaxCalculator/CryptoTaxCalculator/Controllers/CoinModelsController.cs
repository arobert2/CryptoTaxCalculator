using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CryptoTaxCalculator.Models;
using CryptoTaxCalculator.Services;

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
            foreach (CoinModel c in converted)
                Create(c);

        }      

        // POST: CoinModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TimeStamp,Cap24hrChange,Name,MarketCap,Percent,Price,Shapeshift,Symbol,Supply,UsdVolume,Volume,VwapData,VwapDataBtc")] CoinModel coinModel)
        {
            if (ModelState.IsValid)
            {
                coinModel.TimeStamp = DateTime.Now;
                _context.Add(coinModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(coinModel);
        }
    }
}
