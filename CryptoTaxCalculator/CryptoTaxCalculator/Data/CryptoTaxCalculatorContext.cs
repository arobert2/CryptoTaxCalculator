using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CryptoTaxCalculator.Models
{
    public class CryptoTaxCalculatorContext : DbContext
    {
        public CryptoTaxCalculatorContext (DbContextOptions<CryptoTaxCalculatorContext> options)
            : base(options)
        {
        }

        public DbSet<CryptoTaxCalculator.Models.CoinModel> CoinModel { get; set; }
    }
}
