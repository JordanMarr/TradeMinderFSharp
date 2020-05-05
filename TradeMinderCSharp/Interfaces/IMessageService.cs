using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TradeMinderCSharp.Interfaces
{
    public interface IMessageService
    {
        Task SendMessage(Entities.Message message);
    }
}
