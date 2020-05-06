using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TradeMinderCSharp.Entities;
using TradeMinderCSharp.Interfaces;

namespace TradeMinderCSharp.Services
{
    public class Database : Interfaces.IDatabase
    {
        private IConfig _config;

        public Database(IConfig config)
        {
            _config = config;
        }

        /// <summary>
        /// Gets data info from the database
        /// </summary>
        public async Task<NotificationThresholds> GetThresholds(string symbol, string email)
        {
            // TODO: Implement a real ADO.NET or Dapper connection here...
            //var connStr = _config.GetConnectionString();

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
