using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TradeMinderCSharp.Entities;

namespace TradeMinderCSharp.Services
{
    public class StockApi : Interfaces.IStockApi
    {
        /// <summary>
        /// Gets the latest stock info for the given symbol.
        /// </summary>
        public async Task<StockInfo> GetLatest(string symbol)
        {
            await Task.Delay(1000);
            if (symbol == "MSFT")
            {
                return new StockInfo { Symbol = "MSFT", Date = DateTime.Now, Value = 56.50 };
            }
            else
            {
                return null;
            }            
        }
    }
}
