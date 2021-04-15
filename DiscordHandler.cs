using Discord;
using Discord.WebSocket;
using InisBot.MessageHandlers;
using System;
using System.Threading.Tasks;

namespace InisBot
{
    internal class DiscordHandler
    {

        private readonly DiscordSocketClient _client;
        private readonly MahCounter _counter;
        private readonly Options _options;
        private readonly Pipeline<MessageContext> _pipeline;


        public DiscordHandler(string discordToken, MahCounter counter)
        {
            this._counter = counter;
            this._options = new Options();

            this._client = this.GetClient(discordToken);
            this._client.MessageReceived += MessageReceivedAsync;
            this._client.StartAsync();

            this._pipeline = new Pipeline<MessageContext>();
            this._pipeline.Add(new ToggleMahConfirmHandler().HandleAsync);
            this._pipeline.Add(new AddMahsHandler().HandleAsync);
            this._pipeline.Add(new GetMahsHandler().HandleAsync);
            this._pipeline.Add(new HelpHandler().HandleAsync);
            this._pipeline.Add(new MahHandler().HandleAsync);
        }

        private Task MessageReceivedAsync(SocketMessage message)
        {
            if (message.Author.Id == this._client.CurrentUser.Id)
                return Task.CompletedTask;

            var context = new MessageContext(this._counter, message, this._options);
            return this._pipeline.Run(context);
        }


        private DiscordSocketClient GetClient(string discordBotToken)
        {
            var client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Verbose
            });
            client.Log += LogAsync;
            client.LoginAsync(TokenType.Bot, discordBotToken);
            return client;
        }

        private Task LogAsync(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

    }
}
