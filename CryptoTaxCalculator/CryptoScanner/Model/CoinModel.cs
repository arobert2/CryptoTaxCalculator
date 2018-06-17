using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoScanner.Model
{
    public class CoinModel
    {
        public DateTime TimeStamp { get; set; }
        public decimal Cap24hrChange { get; set; }
        public string Long { get; set; }
        public long MktCap { get; set; }
        public decimal Perc { get; set; }
        public double Price { get; set; }
        public bool Shapeshift { get; set; }
        public string Short { get; set; }
        public long Supply { get; set; }
        public long UsdVolume { get; set; }
        public long Volume { get; set; }
        public double VwapData { get; set; }
        public double VwapDataBtc { get; set; }
    }
}
