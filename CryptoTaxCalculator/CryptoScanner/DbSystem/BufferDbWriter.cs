using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CryptoScanner.DbSystem
{
    public class BufferDbWriter
    {
        private CancellationToken BufferWriteCancel = new CancellationTokenSource().Token;

        public void StartBufferWrite(CoinDbController coinController)
        {
            CoinDbController coincontroller = coinController;

            while (!BufferWriteCancel.IsCancellationRequested)
            {
                var vals = CoinBuffer.CoinStackBuffer.Keys.ToArray();
                foreach (var csb in vals)
                {
                    if (!coincontroller.CheckIfTableExists(csb))
                        coincontroller.CreateTable(csb);

                    if (CoinBuffer.CoinStackBuffer[csb].Count > 0)
                        coincontroller.Create(CoinBuffer.PopCoinModel(csb));
                }
            }
        }
    }
}
