using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TradeMinderCSharp.Entities;
using YahooFinanceApi;

namespace TradeMinderCSharp.Services
{
    public class StockApi : Interfaces.IStockApi
    {
        /// <summary>
        /// Gets the latest stock info for the given symbol.
        /// </summary>
        public async Task<StockInfo> GetStock(string symbol)
        {
            var securities = await Yahoo.Symbols(symbol).Fields(Field.Symbol, Field.RegularMarketPrice).QueryAsync();
            var stock = securities[symbol];
            
            if (stock != null)
            {
                return new StockInfo { Symbol = symbol, 
                                       Date = DateTime.Now, 
                                       Value = Convert.ToDecimal(stock.RegularMarketPrice) };
            }
            else
            {
                return null;
            }            
        }
    }
}
