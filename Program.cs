using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace InisBot
{
    class Program
    {

        private const string DISCORD_BOT_CONFIG_KEY = "DiscordBotToken";

        static async Task Main()
        {
            string discordToken = GetDiscordToken();
            var client = await GetClient(discordToken);
            new MessageHandler(client, new MahCounter(0));
            await client.StartAsync();

            await Task.Delay(Timeout.Infinite);
        }

        private static string GetDiscordToken()
        {
            var configBuilder = new ConfigurationBuilder()
                            .AddJsonFile("config.json", optional: true)
                            .AddUserSecrets<Program>(optional: true);
            var config = configBuilder.Build();

            var discordToken = config.GetValue<string>(DISCORD_BOT_CONFIG_KEY);
            if (string.IsNullOrWhiteSpace(discordToken))
            {
                throw new InisBotConfigurationException($"Discord Bot Token is not configured");
            }

            return discordToken;
        }

        private static async Task<DiscordSocketClient> GetClient(string discordBotToken)
        {
            var client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Verbose
            });
            client.Log += LogAsync;
            await client.LoginAsync(TokenType.Bot, discordBotToken);
            return client;
        }

        private static Task LogAsync(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
