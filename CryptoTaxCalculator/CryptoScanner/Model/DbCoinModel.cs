using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoScanner.Model
{
    public class DbCoinModel
    {
        public DateTime TimeStamp { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public double Price { get; set; }
    }
}
