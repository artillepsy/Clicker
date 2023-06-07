using System;

namespace Business.Configs
{
    [Serializable] public class BusinessConfig
    {
        public string name = "Business";
        public int startLevel = 0;
        public float earnTime;
        public int startEarnCount;
        public int levelUpCost;

        public UpgradeConfig[] upgradeConfigs = new UpgradeConfig[2];
    }
}