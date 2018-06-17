using CryptoScanner.Model;
using CryptoTaxCalculator.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoScanner
{
    public class ScannerDbContext : DbContext
    {
        public ScannerDbContext(DbContextOptions<ScannerDbContext> options)
        : base(options)
        {
        }

        public DbSet<CoinModel> CoinModel { get; set; }
    }
}
