using System;

namespace Business.Configs
{
    [Serializable] public class UpgradeConfig
    {
        public string name = "Upgrade";
        public int buyCost;
        public int earnMultiplier;
    }
}