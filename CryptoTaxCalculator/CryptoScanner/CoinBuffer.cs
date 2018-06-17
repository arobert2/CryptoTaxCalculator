﻿using CryptoScanner.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoScanner
{
    public static class CoinBuffer
    {
        public static Dictionary<string, Stack<DbCoinModel>> CoinStackBuffer { get; private set; } = new Dictionary<string, Stack<DbCoinModel>>();

        public static void PushCoinCollection(CoinCollection coinCollection)
        {
            foreach(var c in coinCollection)
            {
                if (!CoinStackBuffer.ContainsKey(c.Short))
                    CoinStackBuffer.Add(c.Short, new Stack<DbCoinModel>());
                var dbCoin = new DbCoinModel()
                {
                    TimeStamp = c.TimeStamp,
                    Name = c.Long,
                    Symbol = c.Short,
                    Price = c.Price
                };

                CoinStackBuffer[c.Short].Push(dbCoin);
            }
        }

        public static DbCoinModel PopCoinModel(string symbol)
        {
            return CoinStackBuffer[symbol].Pop();
        }
    }
}