using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TradeMinderCSharp.Interfaces
{
    public interface IDatabase
    {
        Task<Entities.NotificationThresholds> GetThresholds(string symbol, string email);
    }
}
