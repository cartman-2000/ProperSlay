using System;
using SDG;
using Rocket.RocketAPI;

namespace PSlay
{
    public class CommandSlay : IRocketCommand
    {
        public bool RunFromConsole
        {
            get { return true; }
        }

        public string Name
        {
            get { return "slay"; }
        }

        public string Help
        {
            get { return "[playername|SteamID64] - Slays the player."; }
        }

        public void Execute(RocketPlayer caller, params string[] command)
        {
            RocketPlayer target = null;
            // Return help on empty command.
            
            if (command.Length == 0)
            {
                RocketChatManager.Say(caller, this.Help);
                return;
            }

            // Check to see if what they put in the command is a valid playername or SteamID64 number, and fail if it isn't.
            try
            {
                target = RocketPlayer.FromCSteamID(command[0].StringToCSteamID());
            }
            catch
            {
                target = RocketPlayer.FromName(command[0]);
            }

            // Causes the target player to suicide, "if" the player is valid.
            try
            {
                target.Suicide();
            }
            catch
            {
                RocketChatManager.Say(caller, String.Format("Cannot find player: {0}", command[0]));
                return;
            }
            // Run with different messages, depending on whether the command was ran from the console, or by a player. If caller equals null, it was sent from the console.
            if (caller != null)
            {
                RocketChatManager.Say(caller.CSteamID, String.Format("You have slayed player: {0}.", target.CharacterName));
                RocketChatManager.Say(target.CSteamID, String.Format("Admin: {0} has slayed you.", caller.CharacterName));
                RocketChatManager.print(String.Format("Admin: {0} [{1}] ({2}), has slayed: {3} [{4}] ({5})", caller.CharacterName, caller.SteamName, caller.CSteamID, target.CharacterName, target.SteamName, target.CSteamID));
            }
            else
            {
                RocketChatManager.Say(target.CSteamID, "Admin: Console has slayed you.");
                RocketChatManager.print(String.Format("Admin: Console, has slayed: {0} [{1}] ({2})", target.CharacterName, target.SteamName, target.CSteamID));
            }
        }
    }
}
