#region Copyright Notice
// ******************************************************************************************************************
// 
// WolfWhispererClient.PrefixModule.cs © Shadow Wolf Development (SilentWolf6662 & Bambinidk) - All Rights Reserved
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
using Discord;
using Discord.Commands;
namespace WolfWhispererClient.Modules
{
	public class PrefixModule : ModuleBase<SocketCommandContext>
	{
		[Command("ping")]
		public async Task HandlePingCommand()
		{
			await Context.Message.ReplyAsync("PING!");
		}
	}
}
