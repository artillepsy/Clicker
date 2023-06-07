using System;

namespace Business.Configs
{
    /// Container for upgrade info for edit it from inspector
    [Serializable] 
    public class UpgradeConfig
    {
        public string name = "Upgrade";
        public ulong buyCost;
        public int earnMultiplier;
    }
}