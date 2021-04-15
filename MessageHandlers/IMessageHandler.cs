using System;
using System.Threading.Tasks;

namespace InisBot.MessageHandlers
{
    interface IMessageHandler
    {

        Task HandleAsync(MessageContext context, Func<Task> next);

    }
}
