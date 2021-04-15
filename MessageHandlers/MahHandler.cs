using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InisBot.MessageHandlers
{
    class MahHandler : IMessageHandler
    {

        private static readonly Regex _match = new Regex("\\bma+h+\\b|\\bmä+h\\b|:inischMah:|:inismahgif:", RegexOptions.IgnoreCase);

        public async Task HandleAsync(MessageContext context, Func<Task> next)
        {
            var mahMatches = _match.Matches(context.Message.Content);
            if (mahMatches.Count == 0)
                return;

            context.MahCounter.Increment(mahMatches.Count);

            if (context.Options.ConfirmMahs)
                await context.Message.Channel.SendMessageAsync($"Inis' sheep have mäh'd {context.MahCounter.Count} times! MÄÄÄÄÄÄÄH~!");
        }

    }
}
