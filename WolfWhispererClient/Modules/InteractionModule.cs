#region Copyright Notice
// ******************************************************************************************************************
// 
// WolfWhispererClient.InteractionModule.cs © Shadow Wolf Development (SilentWolf6662 & Bambinidk) - All Rights Reserved
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
using Discord.Interactions;
namespace WolfWhispererClient.Modules
{
	public class InteractionModule : InteractionModuleBase<SocketInteractionContext>
	{
		[SlashCommand("ping", "Reacieve a ping message!")]
		public async Task HandlePingCommand()
		{
			await RespondAsync("PING!");
		}
	}
}
