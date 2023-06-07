using System;

namespace Saves.Components
{
    /// Save data contains info about upgrade
    [Serializable]
    public struct UpgradeSaveData
    {
        /// Unique ID for easy search in arrays
        public int index;
        public bool purchased;
    }
}