using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CryptoScanner.Model;
using Microsoft.EntityFrameworkCore;

namespace CryptoTaxCalculator.Models
{
    public class CryptoTaxCalculatorContext : DbContext
    {
        public CryptoTaxCalculatorContext (DbContextOptions<CryptoTaxCalculatorContext> options)
            : base(options)
        {
        }

        public DbSet<CoinModel> CoinModel { get; set; }
    }
}
