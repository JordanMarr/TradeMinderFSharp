using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;
using TradeMinderCSharp.Entities;
using TradeMinderCSharp.Interfaces;

namespace TradeMinderCSharp.UnitTests
{
    [TestClass]
    public class FullTests
    {
        [TestMethod]
        public void FullTest_WhenStockIsWithinThresholds_ShouldNotCreateMessage()
        {
            // Prepare return values
            var stock = new StockInfo { Symbol = "MSFT", Date = DateTime.Now, Value = 10.0M };
            var thresholds = new NotificationThresholds { Symbol = "MSFT", Email = "jmarr@microdesk.com", High = 15, Low = 4 };

            // Stub the StockApi
            var stockApi = new Mock<IStockApi>();
            stockApi
                .Setup(s => s.GetStock(It.IsAny<string>()))
                .Returns(Task.FromResult(stock))
                .Verifiable();

            // Stub the Database
            var database = new Mock<IDatabase>();
            database
                .Setup(d => d.GetThresholds(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(thresholds));

            // Stub the MessageService
            var messageSvc = new Mock<IMessageService>();
            messageSvc
                .Setup(m => m.SendMessage(It.IsAny<Message>()));

            // Build up feature class
            var notifier = new StockThresholdNotifier(database.Object, stockApi.Object, messageSvc.Object);

            // Run
            notifier.CheckStock("MSFT", "jmarr@microdesk.com")
                .Wait();

            // Assert expected argument value
            stockApi.Verify(s => s.GetStock("MSFT"));

            // Assert expected argument values
            database.Verify(d => d.GetThresholds("MSFT", "jmarr@microdesk.com"));
        }
    }
}
