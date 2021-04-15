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
        private const string PERSISTANCE_PATH_CONFIG_KEY = "PersistencePath";

        static async Task Main()
        {
            var config = GetConfig();

            string discordToken = GetDiscordToken(config);

            var countPersister = new CounterPersister(config.GetValue(PERSISTANCE_PATH_CONFIG_KEY, "count.json"));
            var mahCounter = await countPersister.GetCounterAsync();
            new DiscordHandler(discordToken, mahCounter);

            await Task.Delay(Timeout.Infinite);
        }

        private static string GetDiscordToken(IConfigurationRoot config)
        {
            var discordToken = config.GetValue<string>(DISCORD_BOT_CONFIG_KEY);
            if (string.IsNullOrWhiteSpace(discordToken))
            {
                throw new InisBotConfigurationException($"Discord Bot Token is not configured");
            }

            return discordToken;
        }

        private static IConfigurationRoot GetConfig()
        {
            var configBuilder = new ConfigurationBuilder()
                            .AddJsonFile("config.json", optional: true)
                            .AddUserSecrets<Program>(optional: true);
            var config = configBuilder.Build();
            return config;
        }

    }
}
