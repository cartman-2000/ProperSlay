using Rocket.RocketAPI;
using SDG;
using System;

namespace PSlay
{
    public class ProperSlay : RocketPlugin
    {
        public static ProperSlay Instance;

        protected override void Load()
        {
            Instance = this;
        }
    }
}
