using System;

namespace Saves.Components
{
    [Serializable]
    public struct BusinessSaveData
    {
        public int level;
        public float earnFillAmount;
        public bool[] upgradesPurchases;
    }
}