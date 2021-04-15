using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InisBot.MessageHandlers
{
    class ToggleMahConfirmHandler : IMessageHandler
    {

        private static readonly Regex _match = new Regex("^!togglema+hconfirms?\\b\\s?(\\d+)?", RegexOptions.IgnoreCase);

        public Task HandleAsync(MessageContext context, Func<Task> next)
        {
            if (!_match.IsMatch(context.Message.Content))
                return next();

            if (context.Options.ConfirmMahs)
            {
                context.Options.ConfirmMahs = false;
                return context.Message.Channel.SendMessageAsync($"I'll no longer confirm määhs. Määh in peace sheep!");
            }
            context.Options.ConfirmMahs = true;
            return context.Message.Channel.SendMessageAsync($"I'll confirm every määh from now on! :inispog:");
        }

    }
}
