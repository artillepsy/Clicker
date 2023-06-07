using System;

namespace Business.Configs
{
    /// Config to easily set up all values from editor
    [Serializable]
    public class BusinessConfig
    {
        public string name = "Business";
        public int startLevel = 0;
        /// Time for earn money
        public float earnTime;
        /// How much money will player get during one eran tick
        public int startEarnCount;
        /// Start level up cost
        public int levelUpCost;

        /// All upgrades relative to current business entity
        public UpgradeConfig[] upgradeConfigs = new UpgradeConfig[2];
    }
}