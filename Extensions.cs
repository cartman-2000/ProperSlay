using System;

namespace PSlay
{
    public static class Extensions
    {
        // Returns a Steamworks.CSteamID from a string, throws a FormatException if the string isn't a valid unsigned number, or isn't in the valid range.
        public static Steamworks.CSteamID StringToCSteamID(this string sCSteamID)
        {
            ulong ulCSteamID;
            if (ulong.TryParse(sCSteamID, out ulCSteamID))
            {
                if ((ulCSteamID >= (ulong)0x0110000100000000 && ulCSteamID <= (ulong)0x0170000000000000) || ulCSteamID == 0)
                {
                    return (Steamworks.CSteamID)ulCSteamID;
                }
                throw new FormatException(String.Format("Unable to convert {0} to a CSteamID, not in the valid range.", sCSteamID));
            }
            throw new FormatException(String.Format("Unable to convert {0} to a CSteamID, not a valid unsigned number.", sCSteamID));
        }
    }
}
