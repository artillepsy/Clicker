using UnityEngine;

namespace Core.Utils
{
    public static class Utils
    {
        public static int GetEarn(int level, int startEarn, int mult1Percent = 0, int mult2Percent = 0)
        {
            return Mathf.RoundToInt(level * startEarn * (1f + mult1Percent / 100f + mult2Percent / 100f));
        }

        public static int GetLevelCost(int level, int startCost)
        {
            return startCost * (level + 1);
        }
    }
}