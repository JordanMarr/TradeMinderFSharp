using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TradeMinderCSharp.Interfaces
{
    public interface IStockApi
    {
        Task<Entities.StockInfo> GetLatest(string symbol);
    }
}
