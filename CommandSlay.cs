using System;
using System.Collections.Generic;
using Rocket.API;
using Rocket.Core.Logging;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;

namespace PSlay
{
    public class CommandSlay : IRocketCommand
    {
        public AllowedCaller AllowedCaller
        {
            get { return AllowedCaller.Both; }
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
            // Return help on empty command.
            if (command.Length == 0)
            {
                UnturnedChat.Say(caller, ProperSlay.Instance.Translations.Instance.Translate("slay_command_help"));
                return;
            }

            string combined = string.Join(" ", command);
            string[] slashed = combined.Split('/');

            if (slashed.Length == 2 && slashed[1] == "Screenshot Evidence")
                combined = slashed[0];

            UnturnedPlayer target = UnturnedPlayer.FromName(combined);

            if (combined.Trim() == String.Empty || combined.Trim() == "0")
            {
                UnturnedChat.Say(caller, ProperSlay.Instance.Translations.Instance.Translate("playername_not_found", command[0]), UnityEngine.Color.red);
                return;
            }

            // Causes the target player to suicide, "if" the player is valid.
            try
            {
                if (!target.Dead)
                {
                    //target.Inventory.
                    if (ProperSlay.Instance.Configuration.Instance.ForceItemDropOnSlay && !target.GodMode)
                    {
                        // Force drop clothes.
                        //target.Player.clothing
                        if (target.Player.clothing.shirt != 0)
                        {
                            ItemManager.dropItem(new Item(target.Player.clothing.shirt, 1, target.Player.clothing.shirtQuality, target.Player.clothing.shirtState), target.Player.transform.position, false, true, true);
                        }
                        if (target.Player.clothing.pants != 0)
                        {
                            ItemManager.dropItem(new Item(target.Player.clothing.pants, 1, target.Player.clothing.pantsQuality, target.Player.clothing.pantsState), target.Player.transform.position, false, true, true);
                        }
                        if (target.Player.clothing.hat != 0)
                        {
                            ItemManager.dropItem(new Item(target.Player.clothing.hat, 1, target.Player.clothing.hatQuality, target.Player.clothing.hatState), target.Player.transform.position, false, true, true);
                        }
                        if (target.Player.clothing.backpack != 0)
                        {
                            ItemManager.dropItem(new Item(target.Player.clothing.backpack, 1, target.Player.clothing.backpackQuality, target.Player.clothing.backpackState), target.Player.transform.position, false, true, true);
                        }
                        if (target.Player.clothing.vest != 0)
                        {
                            ItemManager.dropItem(new Item(target.Player.clothing.vest, 1, target.Player.clothing.vestQuality, target.Player.clothing.vestState), target.Player.transform.position, false, true, true);
                        }
                        if (target.Player.clothing.mask != 0)
                        {
                            ItemManager.dropItem(new Item(target.Player.clothing.mask, 1, target.Player.clothing.maskQuality, target.Player.clothing.maskState), target.Player.transform.position, false, true, true);
                        }
                        if (target.Player.clothing.glasses != 0)
                        {
                            ItemManager.dropItem(new Item(target.Player.clothing.glasses, 1, target.Player.clothing.glassesQuality, target.Player.clothing.glassesState), target.Player.transform.position, false, true, true);
                        }
                        target.Player.clothing.thirdClothes.shirt = 0;
                        target.Player.clothing.shirtQuality = 0;
                        target.Player.clothing.thirdClothes.pants = 0;
                        target.Player.clothing.pantsQuality = 0;
                        target.Player.clothing.thirdClothes.hat = 0;
                        target.Player.clothing.hatQuality = 0;
                        target.Player.clothing.thirdClothes.backpack = 0;
                        target.Player.clothing.backpackQuality = 0;
                        target.Player.clothing.thirdClothes.vest = 0;
                        target.Player.clothing.vestQuality = 0;
                        target.Player.clothing.thirdClothes.mask = 0;
                        target.Player.clothing.maskQuality = 0;
                        target.Player.clothing.thirdClothes.glasses = 0;
                        target.Player.clothing.glassesQuality = 0;
                        target.Player.clothing.thirdClothes.isVisual = true;
                        target.Player.clothing.shirtState = new byte[0];
                        target.Player.clothing.pantsState = new byte[0];
                        target.Player.clothing.hatState = new byte[0];
                        target.Player.clothing.backpackState = new byte[0];
                        target.Player.clothing.vestState = new byte[0];
                        target.Player.clothing.maskState = new byte[0];
                        target.Player.clothing.glassesState = new byte[0];
                        //target.Player.clothing.isSkinned = true;
                        target.Player.clothing.thirdClothes.isMythic = true;
                        target.Player.channel.send("tellClothing", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[] { target.Player.clothing.shirt, target.Player.clothing.shirtQuality, target.Player.clothing.shirtState, target.Player.clothing.pants, target.Player.clothing.pantsQuality, target.Player.clothing.pantsState, target.Player.clothing.hat, target.Player.clothing.hatQuality, target.Player.clothing.hatState, target.Player.clothing.backpack, target.Player.clothing.backpackQuality, target.Player.clothing.backpackState, target.Player.clothing.vest, target.Player.clothing.vestQuality, target.Player.clothing.vestState, target.Player.clothing.mask, target.Player.clothing.maskQuality, target.Player.clothing.maskState, target.Player.clothing.glasses, target.Player.clothing.glassesQuality, target.Player.clothing.glassesState, target.Player.clothing.isVisual, target.Player.clothing.isSkinned, target.Player.clothing.isMythic });

                        // Force drop items.
                        for (byte j = 0; j < PlayerInventory.PAGES - 2; j++)
                        {
                            if (target.Inventory.items[j].getItemCount() > 0)
                            {
                                for (int k = target.Inventory.items[j].getItemCount() - 1; k >= 0; k--)
                                {
                                    ItemJar item = target.Inventory.items[j].getItem((byte)k);
                                    ItemManager.dropItem(item.item, target.Player.transform.position, false, true, true);
                                    target.Inventory.items[j].removeItem((byte)k);
                                }
                            }
                        }
                    }
                    target.Suicide();
                }
                else
                {
                    UnturnedChat.Say(caller, ProperSlay.Instance.Translate("player_is_dead", target.CharacterName), UnityEngine.Color.red);
                    return;
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                UnturnedChat.Say(caller, ProperSlay.Instance.Translations.Instance.Translate("playername_not_found", command[0]), UnityEngine.Color.red);
                return;
            }
            // Run with different messages, depending on whether the command was ran from the console, or by a player. If caller equals null, it was sent from the console.
            if (!(caller is ConsolePlayer))
            {
                UnturnedPlayer unturnedCaller = (UnturnedPlayer)caller;
                UnturnedChat.Say(caller, ProperSlay.Instance.Translations.Instance.Translate("admin_slay_caller", target.CharacterName), UnityEngine.Color.yellow);
                UnturnedChat.Say(target, ProperSlay.Instance.Translations.Instance.Translate("admin_slay_target", unturnedCaller.CharacterName), UnityEngine.Color.yellow);
                Logger.Log(ProperSlay.Instance.Translations.Instance.Translate("admin_slay_log", unturnedCaller.CharacterName, unturnedCaller.SteamName, unturnedCaller.CSteamID, target.CharacterName, target.SteamName, target.CSteamID));
            }
            else
            {
                UnturnedChat.Say(target, ProperSlay.Instance.Translations.Instance.Translate("console_slay_target"), UnityEngine.Color.yellow);
                Logger.Log(ProperSlay.Instance.Translations.Instance.Translate("console_slay_log", target.CharacterName, target.SteamName, target.CSteamID));
            }
        }
    }
}
