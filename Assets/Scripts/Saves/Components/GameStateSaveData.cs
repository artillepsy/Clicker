using System;

namespace Saves.Components
{
    [Serializable]
    public struct GameStateSaveData
    {
        public int timeScale;
        public int moneyCount;
        public BusinessSaveData[] businesses;
    }
}