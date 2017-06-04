using System;
using Rocket.API;

namespace PSlay
{
    public class ProperSlayConfig : IRocketPluginConfiguration
    {
        public bool ForceItemDropOnSlay = false;
        public void LoadDefaults()
        {
        }
    }
}