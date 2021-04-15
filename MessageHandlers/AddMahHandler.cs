using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InisBot.MessageHandlers
{
    class AddMahsHandler : IMessageHandler
    {

        private static readonly Regex _match = new Regex("^!addma+hs?\\b\\s?(\\d+)?", RegexOptions.IgnoreCase);

        public Task HandleAsync(MessageContext context, Func<Task> next)
        {
            var match = _match.Match(context.Message.Content);
            if (!match.Success)
                return next();

            var incrementBy = match.Groups[1].Success ? int.Parse(match.Groups[1].Value) : 1;

            context.MahCounter.Increment(incrementBy);

            return context.Message.Channel.SendMessageAsync($"Inis' sheep have mäh'd {context.MahCounter.Count} times! MÄÄÄÄÄÄÄH~!");
        }

    }
}
