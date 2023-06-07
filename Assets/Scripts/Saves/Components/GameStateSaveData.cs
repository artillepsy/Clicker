using System;

namespace Saves.Components
{
    /// Save data which contains all info about game state
    [Serializable]
    public struct GameStateSaveData
    {
        public int timeScale;
        public int moneyCount;
        public BusinessSaveData[] businesses;
    }
}