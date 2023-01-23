#region Copyright Notice
// ******************************************************************************************************************
// 
// WolfWhispererClient.PrefixHandler.cs © Shadow Wolf Development (SilentWolf6662 & Bambinidk) - All Rights Reserved
// Unauthorized copying of this file, via any medium is strictly prohibited
// Proprietary and confidential
// 
// This work is licensed under the Creative Commons Attribution-NonCommercial-NoDerivs 3.0 Unported License.
// To view a copy of this license, visit http://creativecommons.org/licenses/by-nc-nd/3.0/
// 
// Created & Copyrighted @ 2023-01-23
// 
// ******************************************************************************************************************
#endregion
using System.Diagnostics;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
namespace WolfWhispererClient
{
	public class PrefixHandler
	{
		private readonly DiscordSocketClient _client;
		private readonly CommandService _commands;
		private readonly IConfigurationRoot _config;

		public PrefixHandler(DiscordSocketClient client, CommandService commands, IConfigurationRoot config)
		{
			_client = client;
			_commands = commands;
			_config = config;
		}

		public async Task InitializeAsync()
		{
			_client.MessageReceived += HandleCommandAsync;
		}

		public void AddModule<T>()
		{
			_commands.AddModuleAsync<T>(null);
		}

		private async Task HandleCommandAsync(SocketMessage messageParam)
		{
			try
			{
				var message = messageParam as SocketUserMessage;
				if (message == null) return;

				int argPos = 0;

				char prefix = _config.GetValue("prefix", '!');

				if (!(message.HasCharPrefix(prefix, ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos)) || message.Author.IsBot) return;

				var context = new SocketCommandContext(_client, message);

				await _commands.ExecuteAsync(
					context: context, 
					argPos: argPos, 
					services: null);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"{ex.Message}\n{ex.StackTrace}");
			}
		}
	}
}
