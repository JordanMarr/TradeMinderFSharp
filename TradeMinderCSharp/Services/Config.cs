using System;
using System.Collections.Generic;
using System.Text;

namespace TradeMinderCSharp.Services
{
    public class Config : Interfaces.IConfig
    {
        public string GetConnectionString()
        {
            // System.ConfigurationManager.ConnectionStrings["MyConn"];
            return "conn...";
        }
    }
}
