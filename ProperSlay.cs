using Rocket.API.Collections;
using Rocket.Core.Plugins;

namespace PSlay
{
    public class ProperSlay : RocketPlugin
    {
        public static ProperSlay Instance;

        protected override void Load()
        {
            Instance = this;
        }

        public override TranslationList DefaultTranslations
        {
            get
            {
                return new TranslationList
                {
                    { "slay_command_help", "<\"Playername\"|SteamID64> - Slays a player." },
                    { "too_many_args", "Too many arguments in the command." },
                    { "playername_not_found", "Cannot find player: {0}" },
                    { "admin_slay_caller", "You have slayed player: {0}." },
                    { "admin_slay_target", "Admin: {0} has slayed you." },
                    { "admin_slay_log", "Admin: {0} [{1}] ({2}), has slayed: {3} [{4}] ({5})" },
                    { "console_slay_target", "Admin: Console has slayed you." },
                    { "console_slay_log", "Admin: Console, has slayed: {0} [{1}] ({2})" },
                };
            }
        }
    }
}
