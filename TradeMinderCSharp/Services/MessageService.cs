using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TradeMinderCSharp.Services
{
    public class MessageService : Interfaces.IMessageService
    {
        /// <summary>
        /// Sends an email.
        /// </summary>
        public async Task SendMessage(Entities.Message message)
        {
            Console.Write("Sending message...");
            await Task.Delay(1000);
            Console.WriteLine($"To: {message.Email}\nBody: {message.Body}");
        }
    }
}
