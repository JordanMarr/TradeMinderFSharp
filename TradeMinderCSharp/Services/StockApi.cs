using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TradeMinderCSharp.Entities;

namespace TradeMinderCSharp.Services
{
    public class StockApi : Interfaces.IStockApi
    {
        public async Task<StockInfo> GetLatest(string symbol)
        {
            await Task.Delay(1000);
            if (symbol == "MSFT")
            {
                return new StockInfo { Symbol = "MSFT" };
            }
            else
            {
                return null;
            }            
        }
    }
}
