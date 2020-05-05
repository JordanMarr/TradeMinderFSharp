using System;
using System.Threading.Tasks;
using TradeMinderCSharp.Entities;
using TradeMinderCSharp.Interfaces;

namespace TradeMinderCSharp
{
    public class StockThresholdNotifier
    {
        IDatabase _database;
        IStockApi _stockApi;
        IMessageService _messageSvc;

        public StockThresholdNotifier(IDatabase database, IStockApi stockApi, IMessageService messageSvc)
        {
            _database = database;
            _stockApi = stockApi;
            _messageSvc = messageSvc;
        }

        /// <summary>
        /// This pure function creates a notification (or not) based on the stock info and thresholds.
        /// </summary>
        public Message MaybeCreateMessage(StockInfo stock, NotificationThresholds thresholds)
        {
            if (stock.Value > thresholds.High)
                return new Message { Email = thresholds.Email, Body = $"{stock.Symbol} exceeds the maximum value of ${thresholds.High}." };
            else if (stock.Value < thresholds.Low)
                return new Message { Email = thresholds.Email, Body = $"{stock.Symbol} is less than the minimum value of ${thresholds.Low}." };
            else
                return null;
        }

        /// <summary>
        /// This function contains the logic to run the feature.
        /// </summary>
        public async Task CheckStock(string symbol, string email)
        {
            // 1) IO - Get necessary data
            var stock = await _stockApi.GetLatest(symbol);
            var thresholds = await _database.GetThresholds(symbol, email);

            if (stock != null && thresholds != null)
            {
                // 2) Process business rules to create an alert (or not).
                var message = MaybeCreateMessage(stock, thresholds);

                // 3) IO - Send the message (if one exists)
                if (message != null)
                {
                    Console.WriteLine("Sending message...");
                    await _messageSvc.SendMessage(message);
                }
                else
                {
                    Console.WriteLine("No message was sent.");
                }
            }
            else
            {
                Console.WriteLine("Requires stock and threshold.");
            }
        }
    }
}
