using System;
using System.Collections.Generic;
using System.Text;
using CryptoTaxCalculator.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CryptoScanner
{
    public class ScannerContextFactory : IDesignTimeDbContextFactory<ScannerDbContext>
    {
        public ScannerDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ScannerDbContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=CryptoTaxCalculatorContext-8710fd86-0320-4410-bd1a-f36e7f9c8c48;Trusted_Connection=True;MultipleActiveResultSets=true");
            return new ScannerDbContext(optionsBuilder.Options);
        }        
    }
}
