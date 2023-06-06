using System;

namespace Business
{
    [Serializable] public class BusinessConfig
    {
        public string name;
        public int startLevel = 0;
        public float earnTime;
        public int startEarnCount;
        public int levelUpCost;

        public UpgradeConfig firstUpgradeConfig;
        public UpgradeConfig secondUpgradeConfig;
    }
}