using System;
using System.Collections.Generic;
using Rocket.API;
using Rocket.Core.Logging;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Commands;
using Rocket.Unturned.Player;

namespace PSlay
{
    public class CommandSlay : IRocketCommand
    {
        public bool AllowFromConsole
        {
            get { return true; }
        }

        public string Name
        {
            get { return "slay"; }
        }

        public string Help
        {
            get { return "Slays a player."; }
        }

        public string Syntax
        {
            get { return "<\"Playername\"|SteamID64>"; }
        }

        public List<string> Aliases
        {
            get { return new List<string>(); }
        }

        public List<string> Permissions
        {
            get { return new List<string>() { "ProperSlay.slay" }; }
        }

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer target = command.GetUnturnedPlayerParameter(0);
            // Return help on empty command.
            if (command.Length == 0)
            {
                UnturnedChat.Say(caller, ProperSlay.Instance.Translations.Instance.Translate("slay_command_help"));
                return;
            }

            if (command.Length > 1)
            {
                UnturnedChat.Say(caller, ProperSlay.Instance.Translations.Instance.Translate("too_many_args"));
                return;
            }

            if (command[0].Trim() == String.Empty || command[0].Trim() == "0")
            {
                UnturnedChat.Say(caller, ProperSlay.Instance.Translations.Instance.Translate("playername_not_found", command[0]));
                return;
            }

            // Causes the target player to suicide, "if" the player is valid.
            try
            {
                target.Suicide();
            }
            catch
            {
                UnturnedChat.Say(caller, ProperSlay.Instance.Translations.Instance.Translate("playername_not_found", command[0]));
                return;
            }
            // Run with different messages, depending on whether the command was ran from the console, or by a player. If caller equals null, it was sent from the console.
            if (!(caller is ConsolePlayer))
            {
                UnturnedPlayer unturnedCaller = (UnturnedPlayer)caller;
                UnturnedChat.Say(caller, ProperSlay.Instance.Translations.Instance.Translate("admin_slay_caller", target.CharacterName));
                UnturnedChat.Say(target, ProperSlay.Instance.Translations.Instance.Translate("admin_slay_target", unturnedCaller.CharacterName));
                Logger.Log(ProperSlay.Instance.Translations.Instance.Translate("admin_slay_log", unturnedCaller.CharacterName, unturnedCaller.SteamName, unturnedCaller.CSteamID, target.CharacterName, target.SteamName, target.CSteamID));
            }
            else
            {
                UnturnedChat.Say(target, ProperSlay.Instance.Translations.Instance.Translate("console_slay_target"));
                Logger.Log(ProperSlay.Instance.Translations.Instance.Translate("console_slay_log", target.CharacterName, target.SteamName, target.CSteamID));
            }
        }
    }
}
