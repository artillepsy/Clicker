using System;

namespace Saves.Components
{
    /// Save data for each business entity
    [Serializable]
    public struct BusinessSaveData
    {
        /// ID for easy search
        public int index;
        public int level;
        public float earnCurrentTime;
        /// Info about all relative upgrades
        public UpgradeSaveData[] upgrades;
    }
}