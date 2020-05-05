using System;
using System.Collections.Generic;
using System.Text;

namespace TradeMinderCSharp.Entities
{
    public class StockInfo
    {
        public string Symbol { get; set; }
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
    }
}
