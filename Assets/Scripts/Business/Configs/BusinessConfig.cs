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

        public UpgradeConfig upgrade1Config = new UpgradeConfig() {name = "Upgrade 1"};
        public UpgradeConfig upgrade2Config = new UpgradeConfig() {name = "Upgrade 2"};
    }
}