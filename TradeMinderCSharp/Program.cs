using System;
using TradeMinderCSharp.Services;

namespace TradeMinderCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 2)
            {
                var symbol = args[0];
                var email = args[1];

                var notifier = new StockThresholdNotifier(
                    new Database(new Config()),
                    new StockApi(),
                    new MessageService());

                notifier.CheckStock(symbol, email).Wait();
            }
            else
            {
                Console.WriteLine("Invalid args.  Expected: 'TradeMinderCSharp.exe MSFT jmarr@microdesk.com'");
            }
        }
    }
}
