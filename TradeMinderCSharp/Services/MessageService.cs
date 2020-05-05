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
        public async Task SendMessage(string email, string body)
        {
            Console.Write("Sending message...");
            await Task.Delay(1000);
            Console.WriteLine($"To: {email}\nBody: {body}");
        }
    }
}
