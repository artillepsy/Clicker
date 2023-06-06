using System;

namespace Business
{
    [Serializable] public class Business
    {
        public string name;
        public int startLevel = 0;
        public float earnDelay;
        public float startEarnCount;
        public int buyCost;

        public Upgrade firstUpgrade;
        public Upgrade secondUpgrade;
    }
}