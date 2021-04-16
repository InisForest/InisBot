using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InisBot.MessageHandlers
{
    class SetMahsHandler : IMessageHandler
    {

        private static readonly Regex _match = new Regex("^!setma+hs?\\s+(\\d+)", RegexOptions.IgnoreCase);

        public Task HandleAsync(MessageContext context, Func<Task> next)
        {
            var match = _match.Match(context.Message.Content);
            if (!match.Success)
                return next();

            var setTo = int.Parse(match.Groups[1].Value);

            context.MahCounter.Set(setTo);

            return context.Message.Channel.SendMessageAsync($"Inis' sheep have mäh'd {context.MahCounter.Count} times! MÄÄÄÄÄÄÄH~!");
        }

    }
}
