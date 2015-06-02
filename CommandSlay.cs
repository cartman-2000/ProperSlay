using Rocket.Core.Logging;
using Rocket.Unturned;
using Rocket.Unturned.Commands;
using Rocket.Unturned.Player;
using SDG;
using System;
using System.Collections.Generic;


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
            get { return "Slays the player."; }
        }

        public string Syntax
        {
            get { return "<\"Playername\"|SteamID64>"; }
        }

        public List<string> Aliases
        {
            get { return new List<string>(); }
        }

        public void Execute(RocketPlayer caller, string[] command)
        {
            RocketPlayer target = null;
            // Return help on empty command.
            
            if (command.Length == 0)
            {
                RocketChat.Say(caller, this.Syntax + " - " + this.Help);
                return;
            }

            if (command.Length == 1)
            {
                if (command[0].Contains("/"))
                {
                    command = Parser.getComponentsFromSerial(command[0], '/');
                }
            }

            if (command.Length > 1)
            {
                RocketChat.Say(caller, "Invalid arguments in the command.");
                return;
            }

            if (command[0].Trim() == String.Empty || command[0].Trim() == "0")
            {
                RocketChat.Say(caller, "Invalid player name in command.");
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
                RocketChat.Say(caller, String.Format("Cannot find player: {0}", command[0]));
                return;
            }
            // Run with different messages, depending on whether the command was ran from the console, or by a player. If caller equals null, it was sent from the console.
            if (caller != null)
            {
                RocketChat.Say(caller.CSteamID, String.Format("You have slayed player: {0}.", target.CharacterName));
                RocketChat.Say(target.CSteamID, String.Format("Admin: {0} has slayed you.", caller.CharacterName));
                Logger.Log(String.Format("Admin: {0} [{1}] ({2}), has slayed: {3} [{4}] ({5})", caller.CharacterName, caller.SteamName, caller.CSteamID, target.CharacterName, target.SteamName, target.CSteamID));
            }
            else
            {
                RocketChat.Say(target.CSteamID, "Admin: Console has slayed you.");
                Logger.Log(String.Format("Admin: Console, has slayed: {0} [{1}] ({2})", target.CharacterName, target.SteamName, target.CSteamID));
            }
        }
    }
}
