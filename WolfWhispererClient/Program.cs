using System.Collections;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration.Json;
namespace WolfWhispererClient
{
    internal class Program
    {
        public static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("config.json")
                .Build();

            using IHost host = Host.CreateDefaultBuilder().ConfigureServices((_, services) => services 
                // Add the DiscordSocketClient, along with specifying the GatewayIntents and user caching
                .AddSingleton(config)
                .AddSingleton(x => new DiscordSocketClient(new DiscordSocketConfig
                {
                    GatewayIntents = Discord.GatewayIntents.AllUnprivileged,
                    AlwaysDownloadUsers = true,
                }))
                .AddSingleton(x => new InteractionService(x.GetRequiredService<DiscordSocketClient>()))).Build();

            await RunAsync(host);
        }
        private async Task RunAsync(IHost host)
        {
            using IServiceScope serviceScope = host.Services.CreateScope();
            IServiceProvider provider = serviceScope.ServiceProvider;

            var _client = provider.GetRequiredService<DiscordSocketClient>();

            _client.Log += async(LogMessage msg) => { Console.WriteLine(msg.Message); };

            _client.Ready += async () =>
            {
                Console.WriteLine("Bot Ready");
            };

            string token = File.ReadAllText("token.txt");
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            await Task.Delay(-1);
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