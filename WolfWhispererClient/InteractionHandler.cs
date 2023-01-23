#region Copyright Notice
// ******************************************************************************************************************
// 
// WolfWhispererClient.InteractionHandler.cs © Shadow Wolf Development (SilentWolf6662 & Bambinidk) - All Rights Reserved
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
using System.Reflection;
using Discord.Interactions;
using Discord.WebSocket;
namespace WolfWhispererClient
{
	public class InteractionHandler
	{
		private readonly DiscordSocketClient _client;
		private readonly InteractionService _commands;
		private readonly IServiceProvider _services;

		public InteractionHandler(DiscordSocketClient client, InteractionService commands, IServiceProvider services)
		{
			_client = client;
			_commands = commands;
			_services = services;
		}

		public async Task InitializeAsync()
		{
			await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
			
			_client.InteractionCreated += HandleInteraction;


		}
		private async Task HandleInteraction(SocketInteraction arg)
		{
			try
			{
				var ctx = new SocketInteractionContext(_client, arg);
				await _commands.ExecuteCommandAsync(ctx, _services);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
		}
	}
}
