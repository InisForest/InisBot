using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InisBot.MessageHandlers
{
    class GetMahsHandler : IMessageHandler
    {

        private static readonly Regex _match = new Regex("^!getma+hs?\\b", RegexOptions.IgnoreCase);

        public Task HandleAsync(MessageContext context, Func<Task> next)
        {
            if (!_match.IsMatch(context.Message.Content))
                return next();

            return context.Message.Channel.SendMessageAsync($"Inis' sheep have mäh'd {context.MahCounter.Count} times! MÄÄÄÄÄÄÄH~!");
        }

    }
}
