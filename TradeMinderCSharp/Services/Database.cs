using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TradeMinderCSharp.Entities;

namespace TradeMinderCSharp.Services
{
    public class Database : Interfaces.IDatabase
    {
        public async Task<NotificationThresholds> GetThresholds(string symbol, string email)
        {
            await Task.Delay(1000);
            return new NotificationThresholds
            {
                Symbol = "MSFT",
                High = 75,
                Low = 65,
                Email = "jmarr@microdesk.com"
            };
        }
    }
}
