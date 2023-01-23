using System.Collections;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
namespace WolfWhispererClient
{
    internal class Program
    {
        public static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

        private DiscordSocketClient _client;
        public async Task MainAsync()
        {
            using IHost host = Host.CreateDefaultBuilder().ConfigureServices((_, services) => services 
                // Add the DiscordSocketClient, along with specifying the GatewayIntents and user caching
                .AddSingleton(x => new DiscordSocketClient(new DiscordSocketConfig
                {
                    GatewayIntents = Discord.GatewayIntents.AllUnprivileged,
                    AlwaysDownloadUsers = true,
                }))).Build();


        }
    }

    /*
    * //  You can assign your bot token to a string, and pass that in to connect.
    * //  This is, however, insecure, particularly if you plan to have your code hosted in a public repository.
    * //string token = Environment.GetEnvironmentVariable("DISCORD_BOT_TOKEN");
    * string token = File.ReadAllText("token.txt");
    * 
    * 
    * // Some alternative options would be to keep your token in an Environment Variable or a standalone file.
    * // var token = Environment.GetEnvironmentVariable("NameOfYourEnvironmentVariable");
    * // var token = File.ReadAllText("token.txt");
    * // var token = JsonConvert.DeserializeObject<AConfigurationClass>(File.ReadAllText("config.json")).Token;
    */
}