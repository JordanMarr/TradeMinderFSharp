using System;
using System.Collections.Generic;
using System.Text;

namespace TradeMinderCSharp.Entities
{
    public class NotificationThresholds
    {
        public string Symbol { get; set; }
        public decimal Low { get; set; }
        public decimal High { get; set; }
        public string Email { get; set; }
    }
}
