using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InisBot
{
    internal class MessageHandler
    {

        private DiscordSocketClient _client;
        private readonly MahCounter _counter;
        private static Regex match = new Regex("\\bma+h\\b|\\bmä+h\\b|:inischMah:|:inismahgif:", RegexOptions.IgnoreCase);

        public MessageHandler(DiscordSocketClient client, MahCounter counter)
        {
            this._client = client;
            this._counter = counter;
            this._client.MessageReceived += MessageReceivedAsync;
        }

        private async Task MessageReceivedAsync(SocketMessage message)
        {
            if (message.Author.Id == this._client.CurrentUser.Id)
                return;

            var mahMatches = match.Matches(message.Content);
            if (mahMatches.Count == 0)
                return;

            this._counter.Increment(mahMatches.Count);

            await message.Channel.SendMessageAsync($"Inis' sheep have mäh'd {this._counter.Count} times! MÄÄÄÄÄÄÄH~!");
        }

    }
}
