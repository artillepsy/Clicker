using System;

namespace Saves.Components
{
    [Serializable]
    public struct BusinessSaveData
    {
        public int index;
        public int level;
        public float earnCurrentTime;
        public UpgradeSaveData[] upgrades;
    }
}