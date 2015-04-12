using Rocket.RocketAPI;
using SDG;
using System;

namespace PSlay
{
    class ProperSlay : RocketPlugin
    {
        public static ProperSlay Instance;

        protected override void Load()
        {
            Instance = this;
        }

        public Steamworks.CSteamID StringToCSteamID(string sCSteamID)
        {
            ulong ulCSteamID = 0;
            if (ulong.TryParse(sCSteamID, out ulCSteamID))
            {
                if (!(ulCSteamID >= (ulong)0x0110000100000000 && ulCSteamID <= (ulong)0x0170000000000000))
                {
                    throw new FormatException(String.Format("Unable to convert {0} to a CSteamID, not in the valid range.", sCSteamID));
                }
            }
            else
            {
                throw new FormatException(String.Format("Unable to convert {0} to a CSteamID, not a number.", sCSteamID));
            }
            return (Steamworks.CSteamID)ulCSteamID;
        }
    }
}
